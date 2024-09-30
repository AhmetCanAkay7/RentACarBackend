using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        //Entities katmanı Core katmanını reference ettiği için core katmanı entities'i reference edemez.
        //O yüzden User nesnesini core katmanında tutarız ve her projede olabilecek bir nesne.
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
    }
}
