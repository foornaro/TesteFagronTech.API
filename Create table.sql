CREATE TABLE AcompanhamentoPartida (
    Id int NOT NULL IDENTITY(1,1),
    QuantidadePontos int NOT NULL,
    DataPartida datetime,
    PRIMARY KEY (Id)
);