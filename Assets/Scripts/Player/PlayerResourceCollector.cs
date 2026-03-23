using UnityEngine;

public class PlayerResourceCollector : MonoBehaviour
{

    private int money = 0;
    private int meat = 0;
    private int wood = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MoneyBag"))
        {
            Destroy(collision.gameObject);
            money++;
            Debug.Log("los oro que tengo son " + money + " euracos");
            UIManager.Instance.UpdateMoney(money);
        }

        if (collision.gameObject.CompareTag("Meat"))
        {
            Destroy(collision.gameObject);
            meat++;
            Debug.Log("las carne que tengo son " + meat + " que jambre payo");
            UIManager.Instance.UpdateMeat(meat);

        }


        if (collision.gameObject.CompareTag("Wood"))
        {
            Destroy(collision.gameObject);
            wood++;
            Debug.Log("las madera que tengo"+ wood +" madera");
            UIManager.Instance.UpdateWood(wood);

        }
    }


    public void UpdateAllResources()
    {
        UIManager.Instance.UpdateMoney(money);
        UIManager.Instance.UpdateMeat(meat);
        UIManager.Instance.UpdateWood(wood);
    }


    public int GetMoney() => money;
    public int GetMeat() => meat;
    public int GetWood() => wood;


    public void SetMoney(int amount) => money = amount;
    public void SetMeat(int amount) => meat = amount;
    public void SetWood(int amount) => wood = amount;


}
