//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_HW3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tEmployee()
        {
            this.tConnection = new HashSet<tConnection>();
            this.tDependents = new HashSet<tDependents>();
            this.tRestaurantScores = new HashSet<tRestaurantScores>();
            this.tForumMessage = new HashSet<tForumMessage>();
            this.tForumTitle = new HashSet<tForumTitle>();
            this.tManergerHistory = new HashSet<tManergerHistory>();
            this.tRegistrationDetail = new HashSet<tRegistrationDetail>();
            this.tRestaurant = new HashSet<tRestaurant>();
            this.tSampleProposal = new HashSet<tSampleProposal>();
            this.tTravelDetail = new HashSet<tTravelDetail>();
            this.tVoteDetail = new HashSet<tVoteDetail>();
        }
    
        public int fEp_ID { get; set; }
        public string fEp_Code { get; set; }
        public string fEp_Name { get; set; }
        public string fEp_SSNumber { get; set; }
        public string fEp_Addr { get; set; }
        public string fEp_Tel { get; set; }
        public string fEp_Phone { get; set; }
        public int fId_ID { get; set; }
        public System.DateTime fEp_Seniority { get; set; }
        public int fSe_ID { get; set; }
        public string fEp_Nickname { get; set; }
        public string fEp_Password { get; set; }
        public string fEp_Email { get; set; }
        public byte[] fEp_Picture { get; set; }
        public System.DateTime fEp_Birth { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tConnection> tConnection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tDependents> tDependents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRestaurantScores> tRestaurantScores { get; set; }
        public virtual tIdentity tIdentity { get; set; }
        public virtual tSector tSector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tForumMessage> tForumMessage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tForumTitle> tForumTitle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tManergerHistory> tManergerHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRegistrationDetail> tRegistrationDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRestaurant> tRestaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tSampleProposal> tSampleProposal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tTravelDetail> tTravelDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tVoteDetail> tVoteDetail { get; set; }
    }
}
