//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace integrador_moodle.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FormaPagamento
    {
        public FormaPagamento()
        {
            this.Pagamento = new HashSet<Pagamento>();
            this.BandeiraCartao = new HashSet<BandeiraCartao>();
        }
    
        public int formaPagamentoUID { get; set; }
        public string descricao { get; set; }
    
        public virtual ICollection<Pagamento> Pagamento { get; set; }
        public virtual ICollection<BandeiraCartao> BandeiraCartao { get; set; }
    }
}
