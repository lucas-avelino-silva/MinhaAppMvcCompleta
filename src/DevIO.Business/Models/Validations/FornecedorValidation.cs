using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevIO.Business.Models.Validations.Documentos.ValidacaoDocs;

namespace DevIO.Business.Models.Validations
{   //instalar o pacote fluentvalidation na camada de negocio(business)
    public class FornecedorValidation: AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(x => x.Nome) //PropertyName pega o nome do campo, ou seja a msg vai ser dinamica
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(x => x.TipoFornecedor == TipoFornecedor.PessoaFisica, () => 
            {
                RuleFor(x => x.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                //comparando o resulta, para ver se é igual a true
                RuleFor(x => CpfValidacao.Validar(x.Documento)).Equal(true).WithMessage("O Documento fornecido é inválido.");
            });

            When(x => x.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(x => x.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                //comparando o resulta, para ver se é igual a true
                RuleFor(x => CnpjValidacao.Validar(x.Documento)).Equal(true).WithMessage("O Documento fornecido é inválido.");
            });
        }
    }
}
