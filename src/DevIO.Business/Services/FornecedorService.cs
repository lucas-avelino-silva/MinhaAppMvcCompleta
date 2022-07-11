using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        readonly IFornecedorRepository _fornecedorRepository;
        readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fonecedor)
        {
            //Validar o estado da entidade

            /*
                         jeito facil
            var validador = new FornecedorValidation();

            Retorna um validationResult(classe), tem varios metodos, o mais importante é o isValid, e o Erros que é uma lista de erros
            var result = validador.Validate(fonecedor);
            if (!result.IsValid)
            {
                é só lançar os erros para onde vc quiser
                result.Errors;
            } 
             
            */
            
            if(!ExecutarValidacao(new FornecedorValidation(), fonecedor) && !ExecutarValidacao(new EnderecoValidation(), fonecedor.Endereco)) return;

            //Validar se nao existe outra pessoa com o mesmo documento
            if (_fornecedorRepository.Buscar(f => f.Documento == fonecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return;
            }
            
            await _fornecedorRepository.Adicionar(fonecedor);
        }

        public async Task Atualizar(Fornecedor fonecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fonecedor)) return;

            if (_fornecedorRepository.Buscar(f => f.Documento == fonecedor.Documento && f.Id != fonecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _fornecedorRepository.Atualizar(fonecedor);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados!");
                return;
            }

            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }

}
