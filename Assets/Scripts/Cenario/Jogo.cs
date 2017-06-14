﻿using Assets.Scripts.Regras;
using System;

namespace Assets.Scripts.Cenario
{
	public class Jogo
	{
		private Posicao[] posicoes;
		private IGameStatus status = new RegrasJogo();
		public int PosicaoAtual { get; set; }

		private static Jogo _instance;
		public static Jogo Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Jogo();
				return (_instance);
			}
		}

		public Jogo()
		{
			posicoes = new Posicao[8];
			PosicaoAtual = 0;
		}

		public  Posicao[] Posicoes
		{
			get { return posicoes; }
			set
			{
				if (posicoes.Length > 8)
					throw new ArgumentOutOfRangeException("Limitar Posicao Tabuleiro a apenas 8 elemntos");
				posicoes = value;
			}
		}

		public void IniciarJogo()
		{
			status.IniciarJogo();
			DeslocarTabuleiro();
		}

		private void AtivarObjeto(Posicao posicao)
		{
			posicao.Personagem.RenderizarPersonagem();
			status.ProximaQuestao(posicao.Personagem);
			posicao.Percorrido = true;
		}

		private void InativarObjeto(Posicao posicao)
		{
			posicao.Personagem.OcultarPersonagem();
		}

		public void DeslocarTabuleiro()
		{
			if (PosicaoAtual < posicoes.Length)
			{
				AtivarObjeto(Posicoes[PosicaoAtual]);
			}
		}

		public void ResponderPerguntaAtual(string resposta)
		{
			var resultado = status.ResponderPergunta(resposta);

			if (resultado != Posicao.Desempenho.Nao_Respondido)
			{
				//Atribuições o objeto atual
				Posicoes[PosicaoAtual].Resultado = resultado;
				InativarObjeto(Posicoes[PosicaoAtual]);

				//Vai pro próximo
				PosicaoAtual++;
				
				DeslocarTabuleiro();
			}
		}

	}
}