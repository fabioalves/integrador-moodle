using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using integrador_moodle.Models;

namespace integrador_moodle.Areas.Discente.ViewModels
{
    public class VmMatriculaPagamento
    {
        public Matricula Matricula;
        public Pagamento Pagamento;
        public string situacao;
    }
}