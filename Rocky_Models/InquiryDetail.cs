﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_Models
{
    public class InquiryDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string InquiryHeaderId { get; set; }
        [ForeignKey(nameof(InquiryHeaderId))]
        public InquiryHeader InquiryHeader { get; set; }
        [Required]
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
