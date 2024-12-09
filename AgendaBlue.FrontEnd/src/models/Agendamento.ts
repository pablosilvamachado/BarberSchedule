export class Agendamento {
    id: number | null;
    clienteId: number;
    profissionalId: number;
    servicoId: number;
    data: string; // ISO Date format (YYYY-MM-DD)
    horario: string; // Hora no formato HH:mm
    status: 'pendente' | 'confirmado' | 'cancelado';
    observacoes: string;

    constructor({
        id = null,
        clienteId,
        profissionalId,
        servicoId,
        data = '',
        horario = '',
        status = 'pendente',
        observacoes = '',
    }: {
        id?: number | null;
        clienteId: number;
        profissionalId: number;
        servicoId: number;
        data?: string;
        horario?: string;
        status?: 'pendente' | 'confirmado' | 'cancelado';
        observacoes?: string;
    }) {
        this.id = id;
        this.clienteId = clienteId;
        this.profissionalId = profissionalId;
        this.servicoId = servicoId;
        this.data = data;
        this.horario = horario;
        this.status = status;
        this.observacoes = observacoes;
    }    
}

