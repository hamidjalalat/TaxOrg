using Domain.Anemic.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;


namespace ViewModels.Nazm_tspagents
{
    public class Nazm_tspagentEditForSendViewModelValidator : AbstractValidator<Nazm_tspagentEditForSendViewModel>
    {
        public Nazm_tspagentEditForSendViewModelValidator()
        {
   
        }
    }
}
