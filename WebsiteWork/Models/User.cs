//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteWork.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int user_id { get; set; }
        public string user_login { get; set; }
        public string user_pass { get; set; }
        public string user_nicename { get; set; }
        public string user_email { get; set; }
        public Nullable<System.DateTime> user_datecreated { get; set; }
        public string user_token { get; set; }
        public Nullable<int> user_role { get; set; }
        public Nullable<System.DateTime> user_datelogin { get; set; }
        public Nullable<bool> user_activate { get; set; }
        public string user_image { get; set; }
    }
}