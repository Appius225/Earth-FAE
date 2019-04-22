using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushMortar : MonoBehaviour , Weapon
{
    public int damage = 1;
    public IEnumerator fire(Vector3 pos, Vector3 target)
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        tileData tile;
        tile = grid.objects[(int)Mathf.Floor(target.x), (int)Mathf.Round(target.y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
        if (tile.enemy != null)
        {
            tile.enemy.damage(damage);
            if ((int)Mathf.Round(target.y / 0.75f) > (int)Mathf.Round(pos.y / 0.75f))
            {
                if (target.x > pos.x)
                {
                    if ((int)Mathf.Floor(target.x + 0.5f) < grid.getCols() && (int)Mathf.Round((target.y + 0.75f) / 0.75f) >= 0)
                    {
                        tileData temp = grid.objects[(int)Mathf.Floor(target.x + 0.5f), (int)Mathf.Round((target.y + 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                        if (temp.blocked)
                        {
                            tile.enemy.damage(1);
                        }
                        else if (temp.city)
                        {
                            tile.enemy.damage(1);
                            grid.damageCity(1);
                        }
                        else if (temp.tank != null)
                        {
                            tile.enemy.damage(1);
                            temp.tank.damage(1);
                        }
                        else if (temp.enemy != null)
                        {
                            tile.enemy.damage(1);
                            temp.enemy.damage(1);
                        }
                        else if (temp.isWater)
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if(en != null)
                            {
                                en.transform.position = temp.transform.position;
                                yield return new WaitForSeconds(0.1f);
                                Destroy(e);
                                tile.enemy = null;
                            }
                        }
                        else if (tile.isNull)
                        {

                        }
                        else
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                float elapsedTime = 0.0f;
                                while (elapsedTime < 0.5f)
                                {
                                    en.transform.position = Vector3.Lerp(tile.transform.position, temp.transform.position, elapsedTime / 0.5f);
                                    elapsedTime += Time.deltaTime;
                                    yield return new WaitForEndOfFrame();
                                }
                                en.transform.position = temp.transform.position;
                                temp.enemy = tile.enemy;
                                tile.enemy = null;
                                en.moveAttack(new Vector3(0.5f, 0.75f, 0.0f));
                            }
                        }
                    }
                }
                else
                {
                    if ((int)Mathf.Floor(target.x - 0.5f) < grid.getCols() && (int)Mathf.Round((target.y + 0.75f) / 0.75f) >= 0)
                    {
                        tileData temp = grid.objects[(int)Mathf.Floor(target.x - 0.5f), (int)Mathf.Round((target.y + 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                        if (temp.blocked)
                        {
                            tile.enemy.damage(1);
                        }
                        else if (temp.city)
                        {
                            tile.enemy.damage(1);
                            grid.damageCity(1);
                        }
                        else if (temp.tank != null)
                        {
                            tile.enemy.damage(1);
                            temp.tank.damage(1);
                        }
                        else if (temp.enemy != null)
                        {
                            tile.enemy.damage(1);
                            temp.enemy.damage(1);
                        }
                        else if (temp.isWater)
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                en.transform.position = temp.transform.position;
                                yield return new WaitForSeconds(0.1f);
                                Destroy(e);
                                tile.enemy = null;
                            }
                        }
                        else if (tile.isNull)
                        {

                        }
                        else
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                float elapsedTime = 0.0f;
                                while (elapsedTime < 0.5f)
                                {
                                    en.transform.position = Vector3.Lerp(tile.transform.position, temp.transform.position, elapsedTime / 0.5f);
                                    elapsedTime += Time.deltaTime;
                                    yield return new WaitForEndOfFrame();
                                }
                                en.transform.position = temp.transform.position;
                                temp.enemy = tile.enemy;
                                tile.enemy = null;
                                en.moveAttack(new Vector3(-0.5f, 0.75f, 0.0f));
                            }
                        }
                    }
                }
            }
            else if ((int)Mathf.Round(target.y / 0.75f) < (int)Mathf.Round(pos.y / 0.75f))
            {
                if (target.x > pos.x)
                {
                    if ((int)Mathf.Floor(target.x + 0.5f) < grid.getCols() && (int)Mathf.Round((target.y - 0.75f) / 0.75f) >= 0)
                    {
                        tileData temp = grid.objects[(int)Mathf.Floor(target.x + 0.5f), (int)Mathf.Round((target.y - 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                        if (temp.blocked)
                        {
                            tile.enemy.damage(1);
                        }
                        else if (temp.city)
                        {
                            tile.enemy.damage(1);
                            grid.damageCity(1);
                        }
                        else if (temp.tank != null)
                        {
                            tile.enemy.damage(1);
                            temp.tank.damage(1);
                        }
                        else if (temp.enemy != null)
                        {
                            tile.enemy.damage(1);
                            temp.enemy.damage(1);
                        }
                        else if (temp.isWater)
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                en.transform.position = temp.transform.position;
                                yield return new WaitForSeconds(0.1f);
                                Destroy(e);
                                tile.enemy = null;
                            }
                        }
                        else if (tile.isNull)
                        {

                        }
                        else
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                float elapsedTime = 0.0f;
                                while (elapsedTime < 0.5f)
                                {
                                    en.transform.position = Vector3.Lerp(tile.transform.position, temp.transform.position, elapsedTime / 0.5f);
                                    elapsedTime += Time.deltaTime;
                                    yield return new WaitForEndOfFrame();
                                }
                                en.transform.position = temp.transform.position;
                                temp.enemy = tile.enemy;
                                tile.enemy = null;
                                en.moveAttack(new Vector3(0.5f, -0.75f, 0.0f));
                            }
                        }
                    }
                }
                else
                {
                    if ((int)Mathf.Floor(target.x - 0.5f) < grid.getCols() && (int)Mathf.Round((target.y - 0.75f) / 0.75f) >= 0)
                    {
                        tileData temp = grid.objects[(int)Mathf.Floor(target.x - 0.5f), (int)Mathf.Round((target.y - 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                        if (temp.blocked)
                        {
                            tile.enemy.damage(1);
                        }
                        else if (temp.city)
                        {
                            tile.enemy.damage(1);
                            grid.damageCity(1);
                        }
                        else if (temp.tank != null)
                        {
                            tile.enemy.damage(1);
                            temp.tank.damage(1);
                        }
                        else if (temp.enemy != null)
                        {
                            tile.enemy.damage(1);
                            temp.enemy.damage(1);
                        }
                        else if (temp.isWater)
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                en.transform.position = temp.transform.position;
                                yield return new WaitForSeconds(0.1f);
                                Destroy(e);
                                tile.enemy = null;
                            }
                        }
                        else if (tile.isNull)
                        {

                        }
                        else
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                float elapsedTime = 0.0f;
                                while (elapsedTime < 0.5f)
                                {
                                    en.transform.position = Vector3.Lerp(tile.transform.position, temp.transform.position, elapsedTime / 0.5f);
                                    elapsedTime += Time.deltaTime;
                                    yield return new WaitForEndOfFrame();
                                }
                                en.transform.position = temp.transform.position;
                                temp.enemy = tile.enemy;
                                tile.enemy = null;
                                en.moveAttack(new Vector3(-0.5f, -0.75f, 0.0f));
                            }
                        }
                    }
                }
            }
            else
            {
                if (target.x > pos.x)
                {
                    if ((int)Mathf.Floor(target.x + 1.0f) < grid.getCols())
                    {
                        tileData temp = grid.objects[(int)Mathf.Floor(target.x + 1.0f), (int)Mathf.Round(target.y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                        if (temp.blocked)
                        {
                            tile.enemy.damage(1);
                        }
                        else if (temp.city)
                        {
                            tile.enemy.damage(1);
                            grid.damageCity(1);
                        }
                        else if (temp.tank != null)
                        {
                            tile.enemy.damage(1);
                            temp.tank.damage(1);
                        }
                        else if (temp.enemy != null)
                        {
                            tile.enemy.damage(1);
                            temp.enemy.damage(1);
                        }
                        else if (temp.isWater)
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                en.transform.position = temp.transform.position;
                                yield return new WaitForSeconds(0.1f);
                                Destroy(e);
                                tile.enemy = null;
                            }
                        }
                        else if (tile.isNull)
                        {

                        }
                        else
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                float elapsedTime = 0.0f;
                                while (elapsedTime < 0.5f)
                                {
                                    en.transform.position = Vector3.Lerp(tile.transform.position, temp.transform.position, elapsedTime / 0.5f);
                                    elapsedTime += Time.deltaTime;
                                    yield return new WaitForEndOfFrame();
                                }
                                en.transform.position = temp.transform.position;
                                temp.enemy = tile.enemy;
                                tile.enemy = null;
                                en.moveAttack(new Vector3(1.0f, 0.0f, 0.0f));
                            }
                        }
                    }
                }
                else
                {
                    if ((int)Mathf.Floor(target.x - 1.0f) >= 0)
                    {
                        tileData temp = grid.objects[(int)Mathf.Floor(target.x - 1.0f), (int)Mathf.Round(target.y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                        if (temp.blocked)
                        {
                            tile.enemy.damage(1);
                        }
                        else if (temp.city)
                        {
                            tile.enemy.damage(1);
                            grid.damageCity(1);
                        }
                        else if (temp.tank != null)
                        {
                            tile.enemy.damage(1);
                            temp.tank.damage(1);
                        }
                        else if (temp.enemy != null)
                        {
                            tile.enemy.damage(1);
                            temp.enemy.damage(1);
                        }
                        else if (temp.isWater)
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                en.transform.position = temp.transform.position;
                                yield return new WaitForSeconds(0.1f);
                                Destroy(e);
                                tile.enemy = null;
                            }
                        }
                        else if (tile.isNull)
                        {

                        }
                        else
                        {
                            GameObject e = tile.enemy.getGameObject();
                            enemyBear en = e.GetComponent(typeof(enemyBear)) as enemyBear;
                            if (en != null)
                            {
                                float elapsedTime = 0.0f;
                                while (elapsedTime < 0.5f)
                                {
                                    en.transform.position = Vector3.Lerp(tile.transform.position, temp.transform.position, elapsedTime / 0.5f);
                                    elapsedTime += Time.deltaTime;
                                    yield return new WaitForEndOfFrame();
                                }
                                en.transform.position = temp.transform.position;
                                temp.enemy = tile.enemy;
                                tile.enemy = null;
                                en.moveAttack(new Vector3(-1.0f, 0.0f, 0.0f));
                            }
                        }
                    }
                }
            }
        }
    }
    public bool[,] tilesHittable(Vector3 position)
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        bool[,] hitTiles = new bool[grid.getCols(), grid.getRows()];
        int x = (int)Mathf.Floor(position.x);
        int y = (int)Mathf.Round(position.y / 0.75f);
        for (int i = 0; i < hitTiles.GetLength(0); i++)
        {
            for (int j = 0; j < hitTiles.GetLength(1); j++)
            {
                if ((j - y) == 0)
                {
                    if (i != x)
                    {
                        hitTiles[i, j] = true;
                    }
                }
                else if ((j % 2) == 1)
                {
                    if (Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == Mathf.Round(((i + 0.5f) - position.x) / 0.5f))
                    {
                        hitTiles[i, j] = true;
                    }
                    else if (Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == -Mathf.Round(((i + 0.5f) - position.x) / 0.5f))
                    {
                        hitTiles[i, j] = true;
                    }
                    else
                    {
                        hitTiles[i, j] = false;
                    }
                }
                else
                {
                    if (Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == Mathf.Round(((i - position.x) / 0.5f)))
                    {
                        hitTiles[i, j] = true;
                    }
                    else if (Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == -Mathf.Round(((i - position.x) / 0.5f)))
                    {
                        hitTiles[i, j] = true;
                    }
                    else
                    {
                        hitTiles[i, j] = false;
                    }
                }
            }
        }
        return hitTiles;
    }
}
