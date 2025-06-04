# KGA_0528_개인 프로젝트

제목: For The King

설명:  좀비 타워 디펜스 류, 모든 좀비들을 처치해서 왕관을 사수 해야합니다.

GitHub 주소 : [https://github.com/tiga1207/3D-Defence](https://github.com/tiga1207/3D-Defence)

**[ 개발자 ]**    김승태

**[ 개발 인원 ]**    1명

**[ 개발 기간 ]**    2025.05.28 ~ 2025.06.04

**[ 개발 환경 ]**   Unity 2022.3.61f1 URP, Visual Studio Code, 

**[ 사용 언어 ]**    C#

**[ 담당 역할 ]** 

1. 플레이어 및 적 NPC, 게임 로직 개발
2. UI 디자인
3. Animator를 통한 캐릭터 애니메이션 구현
4. Particle을 통한 각종 효과 구현
5. 카메라 효과 관리  
6. 사운드 관리

### 1.StartScene

### 1. 메인 화면
<img width="747" alt="1" src="https://github.com/user-attachments/assets/ca0edd22-73c4-4be7-a5ab-66dff38f5a2f" />

![1.png](https://github.com/user-attachments/assets/ca0edd22-73c4-4be7-a5ab-66dff38f5a2f)

- 앵커(anchor)를 통해 각 요소(Button, Image)를 상대 좌표로 사용하여 반응형 UI 구현했습니다.

### 1.1. Setting 버튼

![2.png](https://github.com/user-attachments/assets/4850b2cb-4c02-4c5b-9fdb-65a20dcc2d64)

- 싱글톤을 사용해 구현한 AudioManager를 통해 사운드 조절 값을 저장하여 전역적으로 관리할 수 있도록 함.
- 마우스 값은 PlayerData에 저장하여 InputSystem의 마우스 값과 곱하여 PlayerModel에서 적용할 수 있도록 했습니다.

### 1.2.  Guide 버튼

![3.png](https://github.com/user-attachments/assets/22ea1f91-bc91-405a-b15b-ee11374ca384)

- 간단한 게임 설명 UI를 구현했습니다.
- 플레이어 이동은 WASD 키를 사용하며, 마우스Delta값을 이용해 화면을 움직일 수 있습니다.

### 2. MainScene

![4.png](https://github.com/user-attachments/assets/780dd983-8fcd-412e-bee5-08cdf45f164e)

- PlayerData에 골드, 공격력을 저장하여 게임 시작 시 플레이어는 해당 값을 적용 받은 상태로 진행 가능합니다.
- Raw Image에 RederTexture를 적용하여 움직이는 캐릭터 선택 화면을 구현했으며, CullingMask를 통해  일부 렌더링을 제한하여 보다 집중적으로 표현했습니다.

### 3.3. Game Scene

![5.png](https://github.com/user-attachments/assets/8ed89362-40a4-4a99-8e35-0ff249ec4a7b)

![6.png](https://github.com/user-attachments/assets/1438b027-5239-4c0e-9f9e-817a223f064d)

### 

![7.png](https://github.com/user-attachments/assets/1947b437-8c6a-437a-a53d-fe3f75546ab8)

![8.png](https://github.com/user-attachments/assets/e41ecac8-1c26-4ec2-999d-fc5cc023ead3)

### 4. Monster

![9.png](https://github.com/user-attachments/assets/fcbcd3b1-4404-4422-9f21-410348b4d7f2)

![10.png](https://github.com/user-attachments/assets/fb88e7a4-a093-403b-bb2c-92fbde00e4d3)

- 몬스터는 넥서스를 파괴하기 위해 생성되며, 공격 대상이 있을 경우 플레이어 또는 타워를 추격 후 공격하며, 몬스터가 넥서스에 도달하면 게임 클리어에 실패한다.

### ✅  기능

- **상태**
    - 이동 상태: 넥서스를 향해 이동
    - 추적 상태: 플레이어나 타워 감지 시 해당 타겟 추적
    - 공격 상태: 사거리 안에 들어오면 공격 수행
- **공격 방식**
    - 근거리/원거리 몬스터 분리
    - 공격 애니메이션과 Hitbox 또는 투사체 연동
- **사망 처리**
    - 피격 시 데미지 반영 및 피격 애니메이션 재생
    - HP가 0 이하가 되면 사망 애니메이션 후 파괴
    - 코인 프리팹을 y+1 위치에 생성
    - 이벤트 호출을 통해 GameManager에 사망 알림
- **UI 연동**
    - 몬스터 남은 수는 GameManager에서 UI로 표시
- **NavMesh 기반 자동 경로 탐색**
    - Unity의 **NavMeshAgent**를 활용하여, 플레이어나 넥서스까지 장애물을 피하며 이동
    - 이동 대상은 타겟의 생존 여부 혹은 검출 여부에 따라 동적으로 변경됨
- **애니메이션 연동**
    - 이동, 공격, 피격, 사망 애니메이션이 상태와 연동되어 전환

### ✅ 몬스터 타입

- **근접 몬스터**
    - 플레이어 혹은 타워에 일정 거리 이상 접근하면 근접 공격 수행
- **원거리 몬스터**
    - 일정 거리에서 멈춘 뒤 원거리 공격 수행
    - 투사체(화살)를 생성하여 타겟을 향해 발사하며, 화살이 플레이어 혹은 타워에 충돌 시 데미지

### ✅ 투사체 구현 (원거리 몬스터용)

- **총알(Projectile) 오브젝트 풀링**
    - 원거리 몬스터가 발사하는 총알도 **오브젝트 풀링**을 통해 관리
    - 매번 Instantiate/Destroy 하지 않고, 풀에서 꺼내어 재사용함으로써 성능 최적화
- **총알 풀링 위치**
    - **총알은 몬스터의 자식 오브젝트가 아닌 루트에 위치한 풀링 오브젝트에서 관리됨**
    - 이를 통해 몬스터의 회전에 영향을 받지 않고 정확한 방향으로 발사 가능

### 4. Tower

![11.png](https://github.com/user-attachments/assets/74d49d73-6d80-4dac-8a67-ab8ba2c90a22)

- 타워는 정해진 생성존에 설치되며, 접근하는 몬스터를 자동으로 감지하고 공격하는 자동화 방어 수단이다.

- **타워 설치 및 상호작용**
    - 플레이어가 타워 생성존에 진입 시 타워 설치 UI (`Press F`)가 활성화
    - 설치 이후 업그레이드 및 판매 가능 (UI를 통해 처리)
    - 설치된 타워는 정해진 공격 범위를 기준으로 근처의 몬스터를 자동 추적
- **자동 사격 시스템**
    - 몬스터가 타워의 감지 범위에 들어오면 일정 주기로 자동 사격
    - 사격 시 총알(`Bullet`)이 생성되어 몬스터를 향해 날아감
    - 발사 속도, 데미지 등은 업그레이드 시 강화 가능
- **총알 및 파티클 구현 방식**
    - **총알(Bullet)**
        - 충돌 시 몬스터에 데미지를 주고 파괴됨
        - **총알은 회전하는 타워의 자식이 아닌 루트(상위 계층) 위치에 풀링 생성**
            
            → 타워의 회전이 총알 위치에 영향을 주지 않도록 하기 위함
            
    - **파티클(Particle)**
        - 총알이 몬스터 또는 다른 오브젝트와 충돌했을 때 시각적 피격 효과 발생
- **오브젝트 풀링(Object Pooling)**
    - **총알과 파티클 모두 오브젝트 풀링으로 구현**
    - 매번 Instantiate/Destroy 하지 않고, 풀에서 꺼내어 재사용 → 성능 최적화
    - 풀링 위치는 게임 루트에 위치하도록 설정해 **타워의 로컬 회전에 영향을 받지 않음**
    - 풀에 여유가 없을 경우 자동으로 추가 생성됨

### 5. 타워 생성존

![12.png](https://github.com/user-attachments/assets/7c35ca8d-5e90-4568-8b5e-98b4e2773a9a)

- 타워를 생성할 수 있는 특정 위치에 존재하며, 플레이어가 진입 시 UI를 통해 상호작용할 수 있다.
- **트리거 감지**
    - 플레이어가 진입 시 'F 키' 입력 유도 텍스트 출력
    - 플레이어가 상호작용 가능 상태(`IsCanInteract`)일 경우, 생성/업그레이드/판매 UI 활성화
- **타워 관리**
    - 타워 설치/업그레이드/판매 기능 구현

### 6. Player

![13.png](https://github.com/user-attachments/assets/94888140-8ea6-48d6-9e3f-eb97fdabd3e3)

- **Input System 기반 조작**
    - `Input System`을 사용하여 키보드, 마우스 입력을 처리
    - 키보드 `WASD`로 이동
    - 마우스 이동으로 시야 회전
    - 마우스 우클릭(오른쪽 클릭)으로 근접 공격
- **공격 시스템**
    - 우클릭을 통해 근접 공격 진행 (근접 무기 Collider 활성화 방식)
    - 쿨타임 및 공격 애니메이션 존재
- **피격 및 무적 처리**
    - 피격 시 HP 감소 및 피격 애니메이션 재생
    - 피격 직후 **일정 시간 동안 무적 상태** 진입
    - 무적 시간은 `invincibleTime`으로 설정 가능
- **사망 처리**
    - HP가 0이 되면 사망 애니메이션 재생
    - 사망 시 `GameManager`를 통해 **게임 오버 처리**
- **상호작용 시스템**
    - 타워 설치 존 진입 시 상호작용 텍스트 (`Press F`) 표시
    - 타워와 상호작용 시 생성/판매/업그레이드 가능
- **UI 연동**
    - 체력 수치는 `Stat<T>` 클래스로 관리되어, 실시간으로 UI에 반영되도록 구현

### 7. 로딩 화면 시스템(비동기 씬 전환)

![14.png](https://github.com/user-attachments/assets/ce633598-49ae-438f-b4c7-c38bcf66a1a5)
- 씬 이동 시 전체 블로킹 없이 로딩 UI를 표시하며 부드럽게 씬을 로드한다.
- **비동기 씬 로딩**
    - `SceneManager.LoadSceneAsync()`를 사용하여 백그라운드에서 로딩
    - `allowSceneActivation`을 `false`로 설정하여 진행률 90%까지 로드
- **로딩 UI 처리**
    - Slider를 사용하여 로딩 진행 시각화
    - `loadingSlider.value`는 실시간으로 갱신됨
