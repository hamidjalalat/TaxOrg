namespace ViewModels.Users
{
    public class UserManageStatisticReportViewModel
    {
        public string UserId { get; set; }
		public string UserFirstName { get; set; }
		public string UserLastName { get; set; }
		public string RegulationConfirm1Count { get; set; } //int for Serach Grid
		public string RegulationConfirm2Count { get; set; }//int
		public string RegulationReject1Count { get; set; }//int
		public string RegulationReject2Count { get; set; }//int
		public string Confirm1Count { get; set; } //int
		public string Confirm2Count { get; set; } //int
		public string Reject1Count { get; set; } //int
		public string Reject2Count { get; set; } //int
		public string UserDiscoveryCount { get; set; } //int
		public string DoingConfirm1Count { get; set; } //int
        public string DoingConfirm2Count { get; set; } //int
	}
}
