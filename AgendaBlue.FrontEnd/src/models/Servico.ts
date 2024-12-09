export class Servico {
    id: number | null;
    nome: string;
    duracao: number; // Duração em minutos
    preco: number;

    constructor({
        id = null,
        nome = '',
        duracao = 30,
        preco = 0,
    }: {
        id?: number | null;
        nome?: string;
        duracao?: number;
        preco?: number;
    }) {
        this.id = id;
        this.nome = nome;
        this.duracao = duracao;
        this.preco = preco;
    }
}
