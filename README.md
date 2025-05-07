# Investments

Usuarios do sistema

Acesso a todas as paginas
usuario: admin
senha: admin

Acesso a alguma paginas
usuario: user
senha: user

O projeto esta configurado para usar 2 tipos de banco de dados: sqlite e mongodb

A mudança deve ser feita no arquivo appsettings.Development.json da API.
<br>
<p>
"DatabaseSettings": {
    "UseMongoDb": true,
    "MongoDB": {
      "ConnectionString": "mongodb://localhost:27017",
      "DatabaseName": "InvestmentsDb"
    },
    "SQLiteCon[](url)nectionString": "Data Source=investments.db"
  }
</p>
