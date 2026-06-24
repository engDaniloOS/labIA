using Comum.Models;
using SimpleChat;

var usuario = Usuario.GetUsuarioTeste();

DietaMacro? dietaMacro = await ChatDietaMacro.Execute(usuario);
Console.WriteLine(dietaMacro);

List<DietaRefeicao> dietaRefeicoes = await ChatDietaRefeicao.Execute(dietaMacro);

foreach (var refeicao in dietaRefeicoes)
    Console.WriteLine(refeicao);
