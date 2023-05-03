# Inventory 실습

- 게임에서 주로 사용되는 Inventory의 기본 기능들을 포함한 실습 수행.<br><br>

---

## 시연

<img src="https://user-images.githubusercontent.com/47467306/235951459-974eab1c-10ae-4a47-a77f-2822bb36df9a.gif"></img><br>

- 비어있는 슬롯이 존재하지 않으면 아이템을 인벤토리에 추가할 수 없음.
- 장착형 아이템들은 이미 소지하고 있는 것과 상관없이 한 칸의 슬롯을 차지함.
- 소비형 아이템들은 소지하고 있는지 확인하고 존재할 경우 최대 소지 갯수인 10개 이하일 때는 갯수를 증가시켜 획득하고, 10개 이상일 경우 새로운 슬롯에 추가시킴.
- 획득 후 아이템이 존재하는 슬롯에서 드래그를 시작하면 아이템을 이동시킬 수 있음. 비어 있는 슬롯에 드래그드랍시 아이템을 이동시키고, 아이템이 존재하는 슬롯에 이동 시 서로의 위치를 교체시킴.