using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamplesRequest.Models.Models.DAL.DBContext;
using SamplesRequest.Models.Models.Entities;

namespace SamplesRequest.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SamplesRequestDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.SamplesPriorities.Any() && context.SamplesPurposes.Any() && context.Users.Any())
            {
                return;
            }

            var priorities = new SamplePriority[]
            {
                new SamplePriority{name="Low",key="LPI"},
                new SamplePriority{name="Medium",key="MPI"},
                new SamplePriority{name="High",key="HPI"}
            };

            foreach (SamplePriority priority in priorities)
            {
                context.SamplesPriorities.Add(priority);
            }
            context.SaveChanges();

            var purposes = new SamplePurpose[]
            {
                new SamplePurpose{name="Agency Validation",key="APU"},
                new SamplePurpose{name="Customer Validation",key="CPU"},
                new SamplePurpose{name="Internal Validation",key="IPU"}
            };

            foreach(SamplePurpose purpose in purposes)
            {
                context.SamplesPurposes.Add(purpose);
            }
            context.SaveChanges();

            var users = new User[]
            {
                        new User{ uname="WORLD/acarrasc", name="Alejandro Carrasco", email="alejandro.carrasco@xyleminc.com"},
                        new User{ uname="WORLD/Adalberto.Chavez", name="Adalberto Chavez", email="adalberto.chavez@xylemin.com"},
                        new User{ uname="WORLD/Ana.Alvarado", name="Ana Alvarado", email="ana.alvarado@xylemin.com"},
                        new User{ uname="WORDL/Andrew.Lager", name="Andrew Lager", email="andrew.lager@xylemin.com"}
            };

            foreach(User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

        }
        

    }
}


