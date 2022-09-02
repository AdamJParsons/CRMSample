using CRMSample.Domain.Common.Dtos.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Common.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel(ErrorDto errors)
        {
            Errors = errors;
        }

        public ErrorDto Errors { get; }
    }
}
