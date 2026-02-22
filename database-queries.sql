-- Queries Úteis - Sistema de Equipamentos
-- OBS: Entity Framework cria colunas com PascalCase, use aspas duplas

-- Verificar estrutura da tabela
SELECT column_name, data_type, character_maximum_length, is_nullable, column_default
FROM information_schema.columns
WHERE table_name = 'equipamentos'
ORDER BY ordinal_position;

SELECT conname AS constraint_name, contype AS constraint_type, pg_get_constraintdef(oid) AS definition
FROM pg_constraint
WHERE conrelid = 'equipamentos'::regclass;

SELECT indexname, indexdef FROM pg_indexes WHERE tablename = 'equipamentos';

-- Consultas básicas
SELECT * FROM equipamentos;
SELECT COUNT(*) as total FROM equipamentos;
SELECT * FROM equipamentos WHERE "Tipo" = 'Caminhao';
SELECT * FROM equipamentos WHERE "StatusOperacional" = 'Operacional';

SELECT "Codigo", "Modelo", "Horimetro", "StatusOperacional"
FROM equipamentos
ORDER BY "Horimetro" DESC
LIMIT 10;

-- Agregações e estatísticas
SELECT "Tipo", COUNT(*) as quantidade
FROM equipamentos
GROUP BY "Tipo"
ORDER BY quantidade DESC;

SELECT "StatusOperacional", COUNT(*) as quantidade
FROM equipamentos
GROUP BY "StatusOperacional";

SELECT "Tipo", COUNT(*) as quantidade,
    ROUND(AVG("Horimetro"), 2) as media_horimetro,
    ROUND(MIN("Horimetro"), 2) as min_horimetro,
    ROUND(MAX("Horimetro"), 2) as max_horimetro
FROM equipamentos
GROUP BY "Tipo"
ORDER BY media_horimetro DESC;

SELECT "LocalizacaoAtual", COUNT(*) as quantidade, STRING_AGG("Codigo", ', ') as codigos
FROM equipamentos
WHERE "LocalizacaoAtual" IS NOT NULL
GROUP BY "LocalizacaoAtual"
ORDER BY quantidade DESC;

-- Consultas de debug
SELECT "Codigo", COUNT(*) FROM equipamentos GROUP BY "Codigo" HAVING COUNT(*) > 1;
SELECT * FROM equipamentos WHERE "Codigo" IS NULL OR "Codigo" = '' OR "Modelo" IS NULL OR "Modelo" = '';
SELECT * FROM equipamentos WHERE "Horimetro" < 0;
SELECT * FROM equipamentos WHERE "Tipo" NOT IN ('Caminhao', 'Escavadeira', 'Perfuratriz', 'Carregadeira', 'Trator');
SELECT * FROM equipamentos WHERE "StatusOperacional" NOT IN ('Operacional', 'EmManutencao', 'Parado');

-- Dashboard resumido
SELECT
    (SELECT COUNT(*) FROM equipamentos) as total_equipamentos,
    (SELECT COUNT(*) FROM equipamentos WHERE "StatusOperacional" = 'Operacional') as operacionais,
    (SELECT COUNT(*) FROM equipamentos WHERE "StatusOperacional" = 'EmManutencao') as em_manutencao,
    (SELECT COUNT(*) FROM equipamentos WHERE "StatusOperacional" = 'Parado') as parados,
    (SELECT ROUND(AVG("Horimetro"), 2) FROM equipamentos) as media_horimetro;

SELECT "Codigo", "Modelo", "DataAquisicao", "Horimetro"
FROM equipamentos
WHERE "DataAquisicao" IS NOT NULL
ORDER BY "DataAquisicao" ASC
LIMIT 5;

SELECT "Codigo", "Modelo", "DataAquisicao", "Horimetro"
FROM equipamentos
WHERE "DataAquisicao" IS NOT NULL
ORDER BY "DataAquisicao" DESC
LIMIT 5;

SELECT "Codigo", "Modelo", "Tipo", "Horimetro", "LocalizacaoAtual"
FROM equipamentos
WHERE "StatusOperacional" = 'Operacional' AND "Horimetro" > 20000
ORDER BY "Horimetro" DESC;
