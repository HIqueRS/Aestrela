using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    public float distanciaProFim;
    public float quantoAndou;
    public float totalSoma;

    public bool estaAberto;

    public PathNode veioDele;

    public bool ehParede;

    public int custoDeAndar;

    public int x, y;

    public Vector2 posicao;


    public PathNode()
    {
        distanciaProFim = float.MaxValue;

        veioDele = null;

        quantoAndou = 0;
        totalSoma = float.MaxValue;
        estaAberto = true;
        ehParede = false;
        custoDeAndar = 1;
    }
}
