﻿namespace TransIT.DAL.Models.Entities
{
    public class IssueLogParts
    {
        public int IssueLogId { get; set; }
        public virtual IssueLog IssueLog { get; set; }
        public int PartId { get; set; }
        public virtual Part Part { get; set; }
    }
}
