using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamplesRequest.Models.Models.DAL.DBContext;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;

namespace SamplesRequest.Repositories
{
    public class WorkflowRepository : GenericRepository<WorkflowStep>, IWorkflowRepository
    {
        public WorkflowRepository(SamplesRequestDBContext context) : base(context)
        {
        }

        public WorkflowStep SaveAndReturnStep(WorkflowStep objToSave)
        {
            if(Save(objToSave))
            return base.GetBy(step => step.id == objToSave.id, includeProperties: "responsibles");

            return null;
        }

        public bool Update(WorkflowStep objToSave, int type)
        {
            if(type == 1)
            {
                var formerStep = GetBy(e => e.order == objToSave.order - 1);
                objToSave.order--;
                formerStep.order++;
                Context.Entry(formerStep).State = EntityState.Modified;
                base.Update(formerStep);
            }
            else
            {
                var nextStep = GetBy(e => e.order == objToSave.order + 1);
                objToSave.order++;
                nextStep.order--;
                Context.Entry(nextStep).State = EntityState.Modified;
                base.Update(nextStep);
            }

            return base.Update(objToSave);
        }

    }
}
