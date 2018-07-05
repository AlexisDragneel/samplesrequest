using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamplesRequest.Models.Models.Entities;

namespace SamplesRequest.Models.Models.DAL.Repositories
{
    public interface IWorkflowRepository:IGenericRepository<WorkflowStep>
    {
         WorkflowStep SaveAndReturnStep(WorkflowStep objToSave);
        bool Update(WorkflowStep objToSave, int type);
    }
}
