import { Component, OnInit } from '@angular/core';


export interface Produto {
  nome: string;
  preco: number;
}

const produtos: Produto[] = [
  { nome: 'Tv', preco: 400 },
  { nome: 'Celular', preco: 400 },
  { nome: 'Torradeira', preco: 400 }
]

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})

export class ProdutosComponent implements OnInit {

  title = 'produtos'

  clickedRows = new Set<Produto>();

  displayedColumns: string[] = ['nome', 'preco'];
  dataSource = produtos;
    
  constructor() { }

  ngOnInit() {
  }
}
