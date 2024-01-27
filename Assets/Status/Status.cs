using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
        public UnityEngine.UI.Image healthBar; 
        public UnityEngine.UI.Image manaBar; 
        public UnityEngine.UI.Image energyBar;
        public float coins;

        public float maxHealth = 100f; 
        public float currentHealth = 100f; 

        public float maxMana = 100f; 
        public float currentMana = 100f; 

        public float maxEnergy = 100f; 
        public float currentEnergy = 100f;
        private float recoveryAmount = 0.12f;
        public Boolean isEnergyZero;

        private float lerpSpeed = 5f;
        void Start()
        {
            UpdateHealthBar();
            UpdateManaBar();
            UpdateEnergyBar();
            coins = 0;
            
            StartCoroutine(RecoverEnergy());
        }
        
        void Update()
        {
            UpdateHealthBar();
            UpdateManaBar();
            UpdateEnergyBar();
        }

        public void UpdateHealthBar()
        {
            float healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthPercentage, lerpSpeed * Time.deltaTime);
            if (currentHealth <= 0f)
            {
                // 在这里可以执行游戏结束的操作，例如显示游戏结束界面、播放游戏结束音效等
                // 停止游戏、显示游戏结束面板等操作
                GameOver();
                gameObject.GetComponent<Animator>().SetBool("isDead",true);
                gameObject.GetComponent<PlayerController>().enabled = false;
            }
        }

        public void UpdateManaBar()
        {
            float manaPercentage = currentMana / maxMana;
            manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, manaPercentage, lerpSpeed * Time.deltaTime);
        }

        public void UpdateEnergyBar()
        {
            float energyPercentage = currentEnergy / maxEnergy;
            energyBar.fillAmount = energyPercentage;
            if (currentEnergy <= 0f && !isEnergyZero)
            {
                isEnergyZero = true;
                StartCoroutine(ResetIsEnergyZero());
            }
        }
        

        // 协程函数，在延迟时间后恢复能量并重新设置isEnergyZero为false
        private IEnumerator ResetIsEnergyZero()
        {
            // 等待两秒钟后将isEnergyZero设置为false
            yield return new WaitForSeconds(0.8f);
            isEnergyZero = false;
        }
        
        private IEnumerator RecoverEnergy()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.01f);
                currentEnergy += recoveryAmount;
                if (currentEnergy >= maxEnergy)
                {
                    currentEnergy = maxEnergy;
                }
                currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
                UpdateEnergyBar();
            }
        }
        
        public void RecoverEnergyAmount(float amount)
        {
            recoveryAmount = amount;
        }

        private IEnumerator BleedingDeBuff()
        {
            while (true)
            {
                Debug.Log("2,3");
                yield return new WaitForSeconds(1f);
                currentHealth -= 1f;
                UpdateHealthBar();
            }
        }

        public void UseRedSkull()
        {
            StartCoroutine(BleedingDeBuff());
            GameObject.FindGameObjectWithTag("Attack").GetComponent<SwordAttack>().swordDamage *= 2;
            Debug.Log("2,2");
        }
        public void StopRedSkull()
        {
            StopCoroutine(BleedingDeBuff());
            GameObject.FindGameObjectWithTag("Attack").GetComponent<SwordAttack>().swordDamage /=2;
        }
        private float originSpeed;
        private IEnumerator BlackChainDeBuff()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.01f);
                currentMana -= 0.05f;
                if (currentMana <= 0)
                {
                    GetComponent<PlayerController>().moveSpeed /= 2;
                    yield break;
                }
                UpdateEnergyBar();
            }
        }

        public void BlackChainBUff()
        {
            originSpeed = GetComponent<PlayerController>().moveSpeed;
            GetComponent<PlayerController>().moveSpeed *= 2;
            StartCoroutine(BlackChainDeBuff());
        }

        public void BlackChainStop()
        {
            GetComponent<PlayerController>().moveSpeed = originSpeed;
            StopCoroutine(BlackChainDeBuff());
        }
        private void GameOver()
        {
            // 在这里可以添加游戏结束的逻辑，例如显示游戏结束界面、播放游戏结束音效等
            // 也可以调用其他管理游戏结束的脚本或组件
            // 示例：显示一个简单的Debug消息
            Debug.Log("Game Over!");
        }
        
        public void UseBloodBottle()
        {
            currentHealth += 30;

                // 限制血量不超过最大值
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                // 更新血条显示
                UpdateHealthBar();
        }
        public void UseUnBloodBottle()
        {
            currentHealth -= 50;

                // 限制血量不超过最大值
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                // 更新血条显示
                UpdateHealthBar();
        }

        public void Usecherry()
        {
            currentHealth += 10;

            // 限制血量不超过最大值
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            // 更新血条显示
            UpdateHealthBar();
        }

        public void UseYellowRIng()
        {
            currentMana += 20;

            // 限制血量不超过最大值
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }

            // 更新血条显示
            UpdateManaBar();
        }
        public void UseManaReturn()
        {
            currentMana += 20;
            currentHealth -= 10;
            UpdateManaBar();
            UpdateHealthBar();
        }
        public void UseLittleSack()
        {
            coins += 20;
        }
        public void UseBigSack()
        {
            coins += 50;
        }

        public void swordAttackUsing()
        {
            currentEnergy -= 20;
            UpdateEnergyBar();
        }
}
