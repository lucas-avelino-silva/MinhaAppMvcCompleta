using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevIO.App.Data;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedor, IMapper map)
        {
            _fornecedorRepository = fornecedor;
            _mapper = map;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel >> (await _fornecedorRepository.ObterTodos()));
        }

        // GET: Fornecedores/Details/5
        public IActionResult Details(Guid id)
        {
            //NAO ESTA MAIS ASSINCRONO ESSA ACTION
            var fornecedorViewModel =  ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(fornecedorViewModel);
            }

            var fornecedorView = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Adicionar(fornecedorView);

            return RedirectToAction("Index");
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            _fornecedorRepository.Atualizar(fornecedor);

            return RedirectToAction("Index");
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await _fornecedorRepository.ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await _fornecedorRepository.ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) { return NotFound(); }

            await _fornecedorRepository.Remover(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            FornecedorViewModel fornecedor = ObterFornecedorEndereco(id);

            if(fornecedor == null) { return NotFound(); }

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        public  FornecedorViewModel ObterFornecedorEndereco(Guid id)
        {
            var sss = _mapper.Map<FornecedorViewModel>( _fornecedorRepository.ObterFornecedorEndereco(id));
            return sss;
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
