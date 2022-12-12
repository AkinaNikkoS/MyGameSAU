using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] PetsPrefabs;
    public GameObject[] ObstaclePrefabs;
    public GameObject CoinPrefab;
    public GameObject GemPrefab;
    public GameObject FirstCoinPrefab;
    public GameObject FirstObstaclePrefab;
    public int MinObstacle;
    public int MaxObstacle;
    public float DistanceBetweenObstacles = 1f;
    public Transform FinishObstacle;
    public Transform AroundRoot;
    public float ExtraRiverScale = 1f;
    public Game Game;
    private List<int> numberGemsPlaces = new List<int>();
    private void Awake()
    {
        Random random = new Random();
        int obstaclesCount = RandomRange(random, MinObstacle, MaxObstacle + 1);
        int coinsCount = RandomRange(random, MinObstacle/10, MaxObstacle/10 + 1);
        int gemsCount = RandomRange(random, MinObstacle / 30 + 1, MaxObstacle / 30 + 2);
        int petsNumber = RandomRange(random, 0, 5);

        for (int i = 1; i < obstaclesCount + 1; i++)
        {
            int prefabIndex = RandomRange(random, 0, ObstaclePrefabs.Length);
            GameObject obstaclePrefab = i == 0 ? FirstObstaclePrefab : ObstaclePrefabs[prefabIndex];
            GameObject obstacle = Instantiate(obstaclePrefab, transform);
            obstacle.transform.localPosition = CalculateObstaclePosition(i, obstacle.transform.localPosition);
        }

        for (int i = 0; i < gemsCount ; i++)
        {             
            numberGemsPlaces.Add(GemPlace(coinsCount + gemsCount, numberGemsPlaces));
            Debug.Log(numberGemsPlaces[i]);
        }
                
        float obstacleToCoins = obstaclesCount / (coinsCount + gemsCount);

        for (int i = 1; i < coinsCount + gemsCount + 1; i++)
        {
            for (int j = 0; j < numberGemsPlaces.Count; j++)
            {
                if (i == numberGemsPlaces[j])
                {
                    GameObject gem = Instantiate(GemPrefab, transform);                    
                    gem.transform.localPosition = CalculateCoinPosition(i, obstacleToCoins, gem.transform.localPosition);
                    break;
                }
                else
                {
                    if (j == numberGemsPlaces.Count - 1)
                    {
                        GameObject coin = Instantiate(CoinPrefab, transform);
                        coin.transform.localPosition = CalculateCoinPosition(i, obstacleToCoins, coin.transform.localPosition);
                    }
                }                
            }            
        }                      
        
        FinishObstacle.localPosition = CalculateObstaclePosition(obstaclesCount + 1, FinishObstacle.transform.localPosition);
        AroundRoot.localScale = new Vector3(1, 1, obstaclesCount * DistanceBetweenObstacles/9 + ExtraRiverScale);
    }

    private int GemPlace (int bonusCount, List<int> gemsPlaces)
    {
        Random rand = new Random();
        int gemsNumber = RandomRange(rand, 1, bonusCount + 1);
        bool k = false;
        if (numberGemsPlaces.Count == 0)
        {
            return gemsNumber;
        }
        else
        {
            for (int j = 0; j < numberGemsPlaces.Count; j++)
                if (gemsNumber != numberGemsPlaces[j])
                {
                    numberGemsPlaces.Add(gemsNumber);
                    k = true;
                }
            if (k == false)
            {
                return GemPlace(bonusCount, gemsPlaces);
            }
            else
            {
                return gemsNumber;
            }
        }        
    }
    private int RandomRange(Random random, int min, int maxExclusive)
    {
        int number = random.Next();
        int length = maxExclusive - min;
        if (length == 0)
            {
                length = 1;
            }
        number %= length;
        return min + number;
    }

    private Vector3 CalculateObstaclePosition(int obstacleIndex, Vector3 obstaclePos)
    {
       return new Vector3(obstaclePos.x, obstaclePos.y, DistanceBetweenObstacles * obstacleIndex);
    }
    private Vector3 CalculateCoinPosition(int indexCoin, float obstacleToCoins, Vector3 coinPos)
    {
        Random random = new Random();
        float coinPosX = RandomRange(random, 0, 7) - 3.5f;
        float coinPosZ = DistanceBetweenObstacles * indexCoin * obstacleToCoins;
        int coinIntPosZ = (int)coinPosZ;
        if (coinIntPosZ % 10 <= 1)            
        {
            coinPosZ += 5f;
        }
        else
        {
            if (coinIntPosZ % 10 >= 9)
            {
                coinPosZ += 5f;
            }    
        } 
        return new Vector3(coinPosX, coinPos.y, coinPosZ);
    }
}
