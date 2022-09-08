using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public PathNode[,] nodos;

    public List<PathNode> closeList;
    public List<PathNode> openList;
    public List<PathNode> pathList;

    public Vector2 posicaoInicial;
    public Vector2 posicaoFinal;

    public int tamX, tamY;

    int[,] maze = new int[19, 22] {
{ 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },//0 XX
{ 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },//1
{ 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },//2
{ 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },//3X
{ 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },//4
{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },//5X
{ 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },//6
{ 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },//7
{ 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },//8
{ 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },//9
{ 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1 },//10
{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1 },//11
{ 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },//12X
{ 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },//13
{ 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1 },//14
{ 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1 },//15X
{ 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },//16
{ 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 },//17X
{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 }//18X
};

    // Start is called before the first frame update
    void Start()
    {
        pathList = new List<PathNode>();
        closeList = new List<PathNode>();
        openList = new List<PathNode>();

        tamX = 19;
        tamY = 22;
        posicaoInicial = new Vector2(0, 1);
        posicaoFinal = new Vector2(18, 20);
        nodos = new PathNode[tamX, tamY];

        //nodos[0, 0];

        //nodos[0, 0];

        for (int i = 0; i < tamX; i++)
        {
            for (int j = 0; j < tamY; j++)
            {
                nodos[i, j] = new PathNode();

                if (maze[i, j] == 1)
                {
                    nodos[i, j].ehParede = true;
                }

            }
        }

        //tem q iniciar se são parede ou nao
        Aestrela(0, 1);

        ShowFinal(18, 20);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aestrela(int x, int y)
    {
        //nodos[x, y] = new PathNode();

        closeList.Add(nodos[x, y]);
        openList.Remove(nodos[x, y]);


        nodos[x, y].estaAberto = false;

        nodos[x, y].posicao = new Vector2(x, y);

        PathNode custoMaisBaixo;

        custoMaisBaixo = new PathNode();

        Vector2 posMB;

        posMB = Vector2.zero;

        bool valido = false;

        if (nodos[x, y].posicao != posicaoFinal)
        {
            if (x - 1 >= 0) //testando o da esqueda
            {
                if (!nodos[x - 1, y].ehParede)
                {
                    if (nodos[x - 1, y].estaAberto)
                    {
                        CalculandoCustos(x - 1, y, x, y);


                        // openList.Add(nodos[x - 1, y]);

                        if (nodos[x - 1, y].totalSoma < custoMaisBaixo.totalSoma)
                        {
                            custoMaisBaixo = nodos[x - 1, y];
                            posMB.x = x - 1;
                            posMB.y = y;

                            valido = true;

                        }

                    }
                }
            }

            if (x + 1 < tamX)//testando direita
            {
                if (!nodos[x + 1, y].ehParede)
                {
                    if (nodos[x + 1, y].estaAberto)
                    {
                        CalculandoCustos(x + 1, y, x, y);


                        if (nodos[x + 1, y].totalSoma < custoMaisBaixo.totalSoma)
                        {
                            custoMaisBaixo = nodos[x + 1, y];
                            posMB.x = x + 1;
                            posMB.y = y;

                            valido = true;
                        }

                    }
                }
            }
            if (y - 1 >= 0) //testando o de baixo
            {
                if (!nodos[x, y - 1].ehParede)
                {
                    if (nodos[x, y - 1].estaAberto)
                    {
                        CalculandoCustos(x, y - 1, x, y);


                        if (nodos[x, y - 1].totalSoma < custoMaisBaixo.totalSoma)
                        {
                            custoMaisBaixo = nodos[x, y - 1];
                            posMB.x = x;
                            posMB.y = y - 1;

                            valido = true;
                        }

                    }
                }
            }

            if (y + 1 < tamY) //testando o de baixo
            {
                if (!nodos[x, y + 1].ehParede)
                {
                    if (nodos[x, y + 1].estaAberto)
                    {
                        CalculandoCustos(x, y + 1, x, y);


                        if (nodos[x, y + 1].totalSoma < custoMaisBaixo.totalSoma)
                        {
                            custoMaisBaixo = nodos[x, y + 1];
                            posMB.x = x;
                            posMB.y = y + 1;

                            valido = true;
                        }

                    }
                }
            }

            custoMaisBaixo.veioDele = nodos[x, y];

            nodos[(int)posMB.x, (int)posMB.y].veioDele = nodos[x, y];

            Debug.Log("indo atras" + x + " " + y);

            //if() se o cara nao achou nenhum lugar decente entao ele pega o primeiro da open list
            if (valido)
            {
                Aestrela((int)custoMaisBaixo.posicao.x, (int)custoMaisBaixo.posicao.y);
            }
            else
            {
                Aestrela((int)openList[0].posicao.x, (int)openList[0].posicao.y);
            }

        }


    }

    public void CalculandoCustos(int x1, int y1, int x2, int y2)
    {


        nodos[x1, y1].posicao = new Vector2(x1, y1);

        nodos[x1, y1].quantoAndou += nodos[x2, y2].quantoAndou + nodos[x2, y2].custoDeAndar;
        nodos[x1, y1].distanciaProFim = Vector2.Distance(nodos[x1, y1].posicao, posicaoFinal);//CalcularDistancia(x - 1, y); // x-1 - x final , y - y final vector2.distr(vetor,verto)
        nodos[x1, y1].totalSoma = nodos[x1, y1].quantoAndou + nodos[x1, y1].distanciaProFim;


        nodos[x1, y1].veioDele = nodos[x2, y2];

        openList.Add(nodos[x1, y1]);
    }

    public void ShowFinal(int x, int y)
    {
        pathList.Add(nodos[x, y]);

        Debug.Log(x + " " + y);

        if (nodos[x, y].veioDele != null)
        {

            ShowFinal((int)nodos[x, y].veioDele.posicao.x, (int)nodos[x, y].veioDele.posicao.y);
        }

        //break;
    }


}
