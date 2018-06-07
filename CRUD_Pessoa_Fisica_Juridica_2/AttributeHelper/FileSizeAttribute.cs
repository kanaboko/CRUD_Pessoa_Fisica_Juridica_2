using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.AttributeHelper
{
    public class FileSizeAttribute : ValidationAttribute
    { 
        private readonly int _maxSize;
        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return (value as HttpPostedFileBase).ContentLength <= _maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("O tamanho do arquivo não pode exceder {0}", _maxSize);
        }
    }
}