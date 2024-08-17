using Infrastructure.MockData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
	
	public class UserMockService
	{
		private static UserMockService _instance;
		public static UserMockService GetInstance()
		{
			if (_instance == null)
				_instance = new UserMockService();
			return _instance;
		}

		private static List<UserClass> lstUser = null;

		public UserMockService()
		{
			if (lstUser == null)
			{
				lstUser = new List<UserClass>()
				{
					new UserClass() { Id = "B20B65B2-ED23-43A5-AA8E-7B46C90EB6CD", FirstName = "مدیر سیستم", LastName = "نظم آران", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "admin", Password = "123456!@#", OrganizationalUnit = "مدیریت" },
					new UserClass() { Id = "7E1331AB-A6C9-4E6D-8DDF-C2BF1DE58DF8", FirstName = "رضا", LastName = "میر براتی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "r.mirbarati", Password = "123456", OrganizationalUnit = "مدیر واحد توسعه نرم افزار" },
					new UserClass() { Id = "B91C2018-92E5-4873-B8F9-2A4167F7FE5F", FirstName = "مجید", LastName = "سامانی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "m.samani", Password = "123456", OrganizationalUnit = "واحد توسعه نرم افزار" },
					new UserClass() { Id = "8A10E738-98C8-4847-B66E-59BCC564FF39", FirstName = "لیلا", LastName = "رضایی", Gender = Domain.Enums.GenderTypeEnum.Female, UserName = "l.rezaei", Password = "123456", OrganizationalUnit = "واحد توسعه نرم افزار" },
					new UserClass() { Id = "131B82D3-F210-4B1E-BD4E-A44CA8B1612D", FirstName = "سعید", LastName = "علیزاده", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "s.alizadeh", Password = "123456", OrganizationalUnit = "واحد توسعه نرم افزار" },
					//new UserClass() { Id = "BC44FE30-2576-4788-8F9E-5BEDF164DDD8", FirstName = "علیرضا", LastName = "مسعودی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "a_masoudi", Password = "am!@#$", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					//new UserClass() { Id = "34CF3940-DB27-4D98-9A71-B58C68FA5161", FirstName = "مهدی", LastName = "داداشی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "m_dadashi", Password = "md$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "BC44FE30-2576-4788-8F9E-5BEDF164DDD8", FirstName = "علیرضا", LastName = "مسعودی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "257659", Password = "am!@#$", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "34CF3940-DB27-4D98-9A71-B58C68FA5161", FirstName = "مهدی", LastName = "داداشی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "320420", Password = "md$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "D1321D1B-9AC0-43FF-A884-E1E74F0E918E", FirstName = "رضا", LastName = "مخدومی جوان", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "257855", Password = "rm$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "3B69B2F0-92C0-4C21-8189-35A658981387", FirstName = "رضا", LastName = "فاتحی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "283668", Password = "rf$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "E8EA68A0-8FDE-4F89-86F6-788B77A2030D", FirstName = "مهران", LastName = "باقر لباف", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "334811", Password = "md$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "7F4B130A-24A2-4B03-A82C-E77706C9B44D", FirstName = "غلامرضا", LastName = "فرد علیرضایی", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "261221", Password = "gf$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
					new UserClass() { Id = "10AEE728-56EB-444B-8421-335E9CF3BF21", FirstName = "فرید", LastName = "ریاض صدری", Gender = Domain.Enums.GenderTypeEnum.Male, UserName = "317416", Password = "fr$#@!", OrganizationalUnit = "اداره مبارزه با پولشویی" },
				};
			}
		}

		public async Task<List<UserClass>> GetAllAsync()
		{
			List<UserClass> result = null;

			await Task.Run(() =>
			{
				result = lstUser;
			});

			return result;
		}


		public async Task<UserClass> findByUserIdAsync(string userId)
		{
			UserClass user = null;

			await Task.Run(() =>
			{
				user = lstUser
					.FirstOrDefault(currnt => currnt.Id.ToLower() == userId)
					;
			});

			return user;
		}


		public async Task<string> GetUserFullNameAsync(string userId)
		{
			string result = string.Empty;

			await Task.Run(async () =>
			{
				UserClass user = await findByUserIdAsync(userId);

				if (user != null)
					result = user.FullName;
			});			

			return result;
		}


		public async Task<UserClass> findByUserNameAsync(string username)
		{
			UserClass user = null;

			await Task.Run(() =>
			{
				user = lstUser
					.FirstOrDefault(currnt => currnt.UserName.ToLower() == username)
					;
			});

			return user;
		}

		public async Task<UserClass> findByUserNamePasswordAsync(string username, string password)
		{
			UserClass user = null;

			await Task.Run(() =>
			{
				user = lstUser
					.FirstOrDefault(currnt => currnt.UserName.ToLower() == username &&
					currnt.Password.ToLower() == password.ToLower())
					;
			});

			return user;
		}
	}
	
}
