-- Sistema de Monitoramento de Equipamentos Pesados
-- ATENÇÃO: Este arquivo é apenas REFERÊNCIA/DOCUMENTAÇÃO
-- O Entity Framework já cria a tabela automaticamente via migrations
-- Para inserir dados de exemplo, use o arquivo database-seed.sql

-- Estrutura da tabela (criada automaticamente pelo EF Core)
CREATE TABLE equipamentos (
    "Id" SERIAL PRIMARY KEY,
    "Codigo" VARCHAR(50) NOT NULL,
    "Tipo" VARCHAR(30) NOT NULL,
    "Modelo" VARCHAR(120) NOT NULL,
    "Horimetro" NUMERIC NOT NULL,
    "StatusOperacional" VARCHAR(30) NOT NULL,
    "DataAquisicao" DATE,
    "LocalizacaoAtual" VARCHAR(200),
    CONSTRAINT chk_horimetro_positivo CHECK ("Horimetro" >= 0),
    CONSTRAINT chk_tipo_valido CHECK ("Tipo" IN ('Caminhao', 'Escavadeira', 'Perfuratriz', 'Carregadeira', 'Trator')),
    CONSTRAINT chk_status_valido CHECK ("StatusOperacional" IN ('Operacional', 'EmManutencao', 'Parado'))
);

CREATE UNIQUE INDEX "IX_equipamentos_Codigo" ON equipamentos ("Codigo");
