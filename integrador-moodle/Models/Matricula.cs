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
    
    public partial class Matricula
    {
        public Matricula()
        {
            this.Certificado = new HashSet<Certificado>();
            this.Pagamento = new HashSet<Pagamento>();
        }
    
        public int matriculaUID { get; set; }
        public int alunoUID { get; set; }
        public int cursoUID { get; set; }
        public int status { get; set; }
    
        public virtual ICollection<Certificado> Certificado { get; set; }
        public virtual ICollection<Pagamento> Pagamento { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
