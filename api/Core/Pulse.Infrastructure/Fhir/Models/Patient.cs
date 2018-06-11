using System.Collections.Generic;
using Pulse.Infrastructure.Fhir.Models.Primitives;

namespace Pulse.Infrastructure.Fhir.Models
{
    public class Patient : BaseModel
    {
        public bool Active { get; set; }

        public IList<Name> Name { get; set; }

        public IList<Telecom> Telecom { get; set; }

        public string Gender { get; set; }

        public string BirthDate { get; set; }

        public IList<Address> Address { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
    }
}