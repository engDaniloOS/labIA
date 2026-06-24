# 🤖 labIA

> Projeto experimental de integração entre **IA** e **.NET**, utilizando OpenAI para criar workflows de automação inteligente.

---

## 📋 Descrição

Este projeto demonstra o uso de inteligência artificial com a plataforma .NET para criar workflows automatizados. O foco principal está em aplicações práticas de IA, como análise de dietas e consultas nutricionistas.

---

## 🎯 Objetivo

Explorar e implementar soluções que combinam:

- 🧠 Inteligência Artificial (OpenAI)
- ⚙️ Workflows de processamento
- 🔀 Lógica de decisão automatizada
- 💻 .NET 10 como base tecnológica

---

## 🚀 Funcionalidades

### Workflows Disponíveis

| Opção | Nome | Descrição |
|-------|------|-----------|
| **1** | `DietaWorkflow` | Processamento linear com análise e geração de planos dietéticos |
| **2** | `NutricionistaWorkflow` | Lógica de decisão integrada com respostas contextualizadas via IA |

---

## 📦 Estrutura do Projeto

```
labIA/
├── OpenAITest/
│   ├── Program.cs              # Ponto de entrada principal
│   └── ...
├── Workflow/
│   ├── Workflows/
│   │   ├── DietaWorkflow.cs
│   │   └── NutricionistaWorkflow.cs
│   └── ...
├── README.md
└── .gitignore
```

---

## 🛠️ Instalação e Configuração

### Pré-requisitos

- ✅ .NET 10 ou superior
- ✅ Visual Studio Community 2026+
- ✅ Chave de API da OpenAI configurada

### Passos para Executar

1. **Clone o repositório:**

   ```powershell
   git clone https://github.com/engDaniloOS/labIA.git
   cd labIA
   ```

2. **Configure suas credenciais da OpenAI:**

   Defina a variável de ambiente `OPENAI_API_KEY` com sua chave de API.

3. **Execute o projeto:**

   ```powershell
   dotnet run --project OpenAITest
   ```

4. **Escolha uma opção:**
   - Digite `1` → Workflow Sequencial
   - Digite `2` → Workflow com Camada de Decisão
   - Digite `sair` → Encerrar aplicação

---

## 💻 Tecnologias

| Tecnologia | Descrição |
|------------|-----------|
| **.NET 10** | Framework base moderno |
| **OpenAI API** | Integração com modelos de IA |
| **C#** | Linguagem principal |

---

## 📝 Exemplo de Uso

```
Digite a opção, sendo 1 para o workflow sequencial e 2 para o workflow com camada de decisão
> 1
[Processando workflow de dieta...]

> 2
[Processando workflow com decisão do nutricionista...]

> sair
[Aplicação encerrada]
```

---

## 🎓 Conceitos Demonstrados

- ✨ Integração com APIs de IA
- 🔄 Padrões de workflows em C#
- ⏳ Processamento assíncrono (`async/await`)
- 🏗️ Arquitetura baseada em camadas de decisão

---

## 👨‍💻 Autor

**engDaniloOS**

- 🔗 GitHub: [@engDaniloOS](https://github.com/engDaniloOS)
- 📦 Repositório: [labIA](https://github.com/engDaniloOS/labIA)

---

## 💬 Exemplos
Mais exemplos de uso no [repositório oficial da microsoft](https://github.com/microsoft/Agent-Framework-Samples/blob/main/README.md)