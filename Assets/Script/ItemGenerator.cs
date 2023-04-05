using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefab������
    public GameObject carPrefab;
    //coinPrefab������
    public GameObject coinPrefab;
    //cornPrefab������
    public GameObject conePrefab;
    //�X�^�[�g�n�_
    private int startPos = 80;
    //�S�[���n�_
    private int goalPos = 360;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;
    //UnityChan�擾
    private GameObject unitychan;
    //Z�̈ʒu�ۑ�
    private float uPositionZ;
    //�A�C�e���̏o���͈�(50m��)
    private float itemGenerationDistance = 50f;
    //�A�C�e���̏o���ʒu
    private float nextItemGenerationPos;

    // Start is called before the first frame update
    void Start()
    {
        //Unity�����̃I�u�W�F�N�g���擾
        this.unitychan = GameObject.Find("unitychan");
        //Unity������z��̏����擾
        this.uPositionZ = unitychan.transform.position.z;
        //���̃A�C�e���̏o���ʒu�����j�e�B�����̈ʒu�{itemGenerationDistance���̈ʒu�ɂ���
        this.nextItemGenerationPos = uPositionZ + itemGenerationDistance;

        // �ŏ��̃A�C�e���𐶐�
        for (int i = startPos; i < uPositionZ + itemGenerationDistance; i += 15)
        {
            GenerateItems(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        uPositionZ = unitychan.transform.position.z;

        if (uPositionZ > nextItemGenerationPos - itemGenerationDistance)
        {
            GenerateItems(nextItemGenerationPos);
            nextItemGenerationPos += 15;
        }

        RemoveItemsBehindUnityChan();

    }


    void GenerateItems(float zPosition)
    {
        //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            for (float j = -1; j <= 1; j += 0.4f)
            {
                //�R�[����x�������Ɉ꒼���ɐ���
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, zPosition);
            }
        }
        else
        {
            //���[�����ƂɃA�C�e���𐶐�
            for (int j = -1; j <= 1; j++)
            {
                //�A�C�e���̎�ނ����߂�
                int item = Random.Range(1, 11);
                //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                int offsetZ = Random.Range(-5, 6);
                //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                if (1 <= item && item <= 6)
                {
                    //�R�C���𐶐�
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, zPosition + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //�Ԃ𐶐�
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, zPosition + offsetZ);
                }
            }
        }
    }

    void RemoveItemsBehindUnityChan()
    {
        float unityChanZPosition = unitychan.transform.position.z;
        // Unity�����������̋���
        float removeDistance = 10f; 

        // �V�[�����̂��ꂼ��̃^�O���I�u�W�F�N�g��S�Ď擾
        GameObject[] cars = GameObject.FindGameObjectsWithTag("CarTag");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("CoinTag");
        GameObject[] cones = GameObject.FindGameObjectsWithTag("TrafficConeTag");

        // CarTag�̃I�u�W�F�N�g���폜
        foreach (GameObject car in cars)
        {
            if (car.transform.position.z < unityChanZPosition - removeDistance)
            {
                Destroy(car);
            }
        }

        // CoinTag�̃I�u�W�F�N�g���폜
        foreach (GameObject coin in coins)
        {
            if (coin.transform.position.z < unityChanZPosition - removeDistance)
            {
                Destroy(coin);
            }
        }

        // TrafficConeTag�̃I�u�W�F�N�g���폜
        foreach (GameObject cone in cones)
        {
            if (cone.transform.position.z < unityChanZPosition - removeDistance)
            {
                Destroy(cone);
            }
        }
    }




}