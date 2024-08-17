using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.EF
{
    public class RegulationGroupRepository : EFRepository<RegulationGroup>, IRegulationGroupRepository
    {
        public RegulationGroupRepository(EFContext context) : base(context)
        {
        }
        public async Task<Result<bool>> IsUnique(RegulationGroup model,CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<bool>();
            List<String> errorList = new List<String>();
            #region Add
            if (model.Id == 0)
            {
                var isExist = await GetAll.AnyAsync(s => s.Code == model.Code, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Code));
                }
                 isExist = await GetAll.AnyAsync(s => s.Title == model.Title,cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Title));
                }
            }
            #endregion

            #region Edit
            if (model.Id > 0)
            {
                var isExist = await GetAll.AnyAsync(s => s.Id != model.Id && s.Code == model.Code,cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Code));
                }
                 isExist = await GetAll.AnyAsync(s => s.Id != model.Id && s.Title == model.Title,cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Title));
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
