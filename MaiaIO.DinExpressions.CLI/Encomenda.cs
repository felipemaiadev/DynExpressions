﻿

using System.Reflection;

namespace MaiaIO.DinExpressions.CLI
{
    public class Encomenda
    {
        public long Id { get; set; }
        public string EmpresaOrigem { get; set; }
        public string EmpresaDestino { get; set; }
        public DateTime PrevisaoChegada { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool IsActive { get; set; }
    }

    public class ListarEncomedaComando
    {
        public long Id { get; set; }
        public string EmpresaOrigem { get; set; }
        public string EmpresaDestino { get; set; }

        [IntervalFilter("Begin", "PrevisaoChegada")]
        public DateTime PrevisaoChegadaInicio { get; set; }

        [IntervalFilter("End", "PrevisaoChegada")]
        public DateTime PrevisaoChegadaFim{ get; set; }

        public DateTime DataCriacao { get; set; }
        public bool IsActive { get; set; }
    }


    public class IntervalFilter : Attribute
    {

        public IntervalFilter(string parameter, string endorbegin)
        {
        }

    }

}
