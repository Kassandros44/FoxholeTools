using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RequestPopup : MonoBehaviour
{

    public string location;

    [SerializeField]
    private Text locationText;
    [SerializeField]
    private GameObject itemButton;
    [SerializeField]
    private GameObject requestListItem;
    [SerializeField]
    private GameObject itemContent;
    [SerializeField]
    private ToggleGroup toggleGroup;
    [SerializeField]
    private GameObject lowProContent, medProContent, highProContent;
    [SerializeField]
    private UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        locationText.text = location;
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

        GenerateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().SetAsLastSibling();
    }

    List<ItemListData> itemListDatas = new List<ItemListData>();

    public void Submit(){

        User user = LocalUser.GetLocalUser().GetComponent<User>();

        for (int i = 0; i < lowProContent.transform.childCount; i++)
        {
            RequestListItem requestListItem = lowProContent.transform.GetChild(i).GetComponent<RequestListItem>();
            ItemListData data = new ItemListData(requestListItem.GetIndex(), int.Parse(requestListItem.GetInputField().text), 1);
            itemListDatas.Add(data);

        }
        for (int i = 0; i < medProContent.transform.childCount; i++)
        {
            RequestListItem requestListItem = medProContent.transform.GetChild(i).GetComponent<RequestListItem>();
            ItemListData data = new ItemListData(requestListItem.GetIndex(), int.Parse(requestListItem.GetInputField().text), 2);
            itemListDatas.Add(data);

        }
        for (int i = 0; i < highProContent.transform.childCount; i++)
        {
            RequestListItem requestListItem = highProContent.transform.GetChild(i).GetComponent<RequestListItem>();
            ItemListData data = new ItemListData(requestListItem.GetIndex(), int.Parse(requestListItem.GetInputField().text), 3);
            itemListDatas.Add(data);

        }

        user.GenerateNewRequest();

        foreach (var item in itemListDatas)
        {
            user.AddNewRequestItem(item.index, item.amount, item.priority);
        }

        user.CreateRequest(location, LocalUser.GetUsername());
        user.UpdateRequestList();

        Destroy(this.gameObject);

    }

    public void Cancel(){

        Destroy(this.transform.gameObject);

    }

    public void GenerateItems(){

        ItemContainer itemContainer = ItemContainer.Load();

        foreach (var item in itemContainer.Items)
        {
            GameObject newItem = Instantiate<GameObject>(itemButton, itemContent.transform);
            newItem.GetComponent<RequestItemBtn>().index = itemContainer.Items.IndexOf(item);
            newItem.GetComponent<RequestItemBtn>().SetRequestPopup(this);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.image);
        }

    }

    public void DoRequest(int index, Sprite sprite){

        ItemContainer itemContainer = ItemContainer.Load();

        foreach (Toggle item in toggleGroup.ActiveToggles())
        {
            if(item.isOn && item.gameObject.name.Contains("Low")){

                IterSpawnListItems(lowProContent, index, sprite);

            } else if (item.isOn && item.gameObject.name.Contains("Medium")){

                IterSpawnListItems(medProContent, index, sprite);

            } else if (item.isOn && item.gameObject.name.Contains("High")){

                IterSpawnListItems(highProContent, index, sprite);

            }
        }

    }

    private void IterSpawnListItems(GameObject content, int index, Sprite sprite){

        if(content.transform.childCount != 0){
                    
                    for (int i = 0; i < content.transform.childCount; i++)
                    {
                        GameObject obj = content.transform.GetChild(i).gameObject;
                        if(obj.GetComponent<RequestListItem>().GetIndex() == index){
                            int curr = int.Parse(obj.GetComponent<RequestListItem>().GetInputField().text);
                            int newint = curr += 1;
                            obj.GetComponent<RequestListItem>().GetInputField().text = newint.ToString();
                            index = 999;
                        } else if (index == 999){
                            break;
                        }
                    } 

                    if(index != 999){
                        GameObject obj = Instantiate<GameObject>(requestListItem, content.transform);
                        RequestListItem newRLI = obj.GetComponent<RequestListItem>();
                        newRLI.SetIndex(index);
                        newRLI.GetInputField().text = "1";
                        newRLI.GetImage().sprite = sprite;
                        
                    }

                } else {

                    GameObject obj = Instantiate<GameObject>(requestListItem, content.transform);
                    RequestListItem newRLI = obj.GetComponent<RequestListItem>();
                    newRLI.SetIndex(index);
                    newRLI.GetInputField().text = "1";
                    newRLI.GetImage().sprite = sprite;

                }

    }

}
