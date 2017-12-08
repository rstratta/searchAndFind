using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Entities
{
    public class TenderImage
    {
        public Guid Id { get; set; }
        public Guid TenderId { get; set; }
        public virtual Tender Tender { get; set; }
        public string EncodedImage { get; set; }

        public TenderImage()
        {
            Id = Guid.NewGuid();
        }

        public TenderImage(string content) : this()
        {
            EncodedImage = content;
        }

        public override bool Equals(object obj)
        {
            return Id.Equals(((TenderImage)obj).Id);
        }
    }
}
