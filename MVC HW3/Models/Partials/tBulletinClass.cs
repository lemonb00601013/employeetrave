using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tBulletinClassMetadata))]
    public partial class tBulletinClass
    {
        public class tBulletinClassMetadata
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]


            [DisplayName("���i���O�s��")]
            public int fBC_ID { get; set; }
            [DisplayName("���i���O")]
            public string fBC_Class { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<tBulletin> tBulletin { get; set; }
        }
    }
}