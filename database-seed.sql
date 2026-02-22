-- Dados de exemplo para popular a tabela equipamentos
-- Execute no DBeaver após a tabela ser criada pelo Entity Framework

INSERT INTO equipamentos ("Id", "Codigo", "Tipo", "Modelo", "Horimetro", "StatusOperacional", "DataAquisicao", "LocalizacaoAtual")
VALUES
    (1, 'CAT-793F-000123', 'Caminhao', 'Caterpillar 793F', 18234.5, 'Operacional', '2019-03-15', 'Mina Carajás N4E'),
    (2, 'KOM-PC5500-001', 'Escavadeira', 'Komatsu PC5500', 12500.0, 'Operacional', '2020-06-20', 'Mina Carajás N4E'),
    (3, 'CAT-D11T-002', 'Trator', 'Caterpillar D11T', 8750.3, 'EmManutencao', '2018-11-10', 'Oficina Central'),
    (4, 'LIE-R9800-003', 'Escavadeira', 'Liebherr R9800', 15420.8, 'Operacional', '2021-02-05', 'Mina Carajás S11D'),
    (5, 'CAT-793F-000124', 'Caminhao', 'Caterpillar 793F', 22150.2, 'EmManutencao', '2017-08-12', 'Oficina Central'),
    (6, 'SAN-DR460i-004', 'Perfuratriz', 'Sandvik DR460i', 5320.0, 'Operacional', '2022-01-18', 'Mina Carajás N5E'),
    (7, 'CAT-785D-005', 'Caminhao', 'Caterpillar 785D', 31240.5, 'Parado', '2015-04-22', 'Pátio Manutenção'),
    (8, 'HITACHI-EX5600-006', 'Escavadeira', 'Hitachi EX5600', 9870.0, 'Operacional', '2023-07-30', 'Mina S11D Corpo Sul'),
    (9, 'CAT-980M-007', 'Carregadeira', 'Caterpillar 980M', 4250.5, 'Operacional', '2023-03-15', 'Mina Carajás N4WN'),
    (10, 'VOLVO-A60H-008', 'Caminhao', 'Volvo A60H', 6780.0, 'EmManutencao', '2022-09-05', 'Oficina Central'),
    (11, 'CAT-793F-000125', 'Caminhao', 'Caterpillar 793F', 14890.0, 'Operacional', '2020-12-08', 'Mina Carajás S11D'),
    (12, 'SAN-DR410i-009', 'Perfuratriz', 'Sandvik DR410i', 3200.0, 'Operacional', '2023-05-22', 'Mina Carajás N5W'),
    (13, 'KOM-PC4000-010', 'Escavadeira', 'Komatsu PC4000', 19500.5, 'Parado', '2016-09-14', 'Pátio Manutenção'),
    (14, 'CAT-988K-011', 'Carregadeira', 'Caterpillar 988K', 7340.0, 'Operacional', '2021-11-03', 'Mina Carajás N4E'),
    (15, 'LIE-T284-012', 'Caminhao', 'Liebherr T284', 25600.3, 'EmManutencao', '2018-02-20', 'Oficina Central');

-- Ajustar sequência do ID para o próximo valor
SELECT setval('equipamentos_Id_seq', 15, true);

-- Verificar dados inseridos
SELECT COUNT(*) as total_equipamentos FROM equipamentos;
SELECT "Tipo", COUNT(*) as quantidade FROM equipamentos GROUP BY "Tipo" ORDER BY quantidade DESC;
SELECT "StatusOperacional", COUNT(*) as quantidade FROM equipamentos GROUP BY "StatusOperacional";
