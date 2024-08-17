using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.EF
{
    public class BranchRepository : EFRepository<Branch>, IBranchRepository
    {
        public BranchRepository(EFContext context) : base(context)
        {
        }
        public async Task<Result<bool>> IsUnique(Branch model,CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<bool>();
            List<String> errorList = new List<String>();

            #region Add
            if (model.Id == 0)
            {
                var isExist = await GetAll.AnyAsync(s => s.Id == model.Id, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Code));
                }
           
            }
            #endregion

            #region Edit
            if (model.Id > 0)
            {
                var isExist = await GetAll.AnyAsync(s => s.Id != model.Id && s.Id == model.Id, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Code));
                }

            }
            #endregion

            if (errorList.Count > 0)
            {
                return result.WithErrors(errorList);
            }
            else
            {
                return result.WithValue(true);
            }
        }
    }


}
