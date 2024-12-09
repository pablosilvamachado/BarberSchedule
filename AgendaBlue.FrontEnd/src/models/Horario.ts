export class Horario {
    id: number | null;
    profissionalId: number;
    diaSemana: string; // Exemplo: 'Segunda-feira'
    inicio: string; // Hora no formato HH:mm
    fim: string; // Hora no formato HH:mm
    intervalo: number; // Intervalo entre atendimentos (em minutos)

    constructor({
        id = null,
        profissionalId,
        diaSemana = '',
        inicio = '',
        fim = '',
        intervalo = 0,
    }: {
        id?: number | null;
        profissionalId: number;
        diaSemana?: string;
        inicio?: string;
        fim?: string;
        intervalo?: number;
    }) {
        this.id = id;
        this.profissionalId = profissionalId;
        this.diaSemana = diaSemana;
        this.inicio = inicio;
        this.fim = fim;
        this.intervalo = intervalo;
    }
}
