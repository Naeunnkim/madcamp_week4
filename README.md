# madcamp_week4: 자유주제 - 게임개발
> 4분반 김나은, 김민재

몰입캠프 4주차 과제는 자유주제로, 우리는 Unity 엔진을 활용해 2D 타워디펜스 게임을 제작했습니다.

<br/>

## 팀원


* 카이스트 전산학부 [김민재](https://github.com/akmj4869)
* 한양대학교 컴퓨터소프트웨어학부 [김나은](https://github.com/Naeunnkim)

<br/>

## 개발 환경
- Unity
- C#

<br/>

## 게임 개요

MadRush<br/><br/>
길을 따라 이동하는 상대편 병사들이 성문을 통과하지 못하도록 막는 게임입니다.<br/>
궁수, 병영, 마법사 타워를 길목에 설치해 상대편 병사들을 공격할 수 있습니다.<br/>
***

### 시작 및 스테이지 선택 화면

<p>
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/b9917974-32b6-413c-b8d1-d013193af7d7" height="32%" width="32%">
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/e9bedb5c-10b5-4f62-894d-ca994853500f" height="32%" width="32%">
</p>
<br/>

- 게임 시작 화면으로 로고 아래 시작 버튼을 누르면 스테이지 선택 화면으로 이동합니다.
- 스테이지 선택 화면에는 3개의 맵이 있으며, 각각의 맵은 적의 공격 횟수와 이동 경로 등에 변화를 주어 난이도에 차이를 주었습니다.

### 맵 화면

<p>
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/ad87473d-cd04-4ac6-8a56-0b3054bc1523" height="32%" width="32%">
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/21cd9442-c9da-4c65-a135-41aaace394c2" height="32%" width="32%">
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/5f1c9a8f-1b99-4b5c-ad5d-397b2f4107e3" height="32%" width="32%">
</p>

- 서로 다른 3개의 맵을 플레이 할 수 있도록 구성했습니다.

### 타워 설명

### 게임 결과 화면

<p>
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/870bb1f3-c6d3-4502-923c-22a09f6f5e53" height="32%" width="32%">
  <img src="https://github.com/Naeunnkim/madcamp_week4/assets/128071056/51f48964-3244-48d8-ae42-2d9b9ec36462" height="32%" width="32%">
</p>

- 적의 이동을 2번 이상 못 막을 시 Game Over 페이지로 이동합니다.
- 모든 wave의 공격을 무사히 막을 경우 You Win 페이지로 이동합니다.
- 각각의 결과 화면에서는 retry 버튼을 눌러 같은 맵을 재시작 할 수 있으며, stage choice 버튼을 눌러 맵 선택 화면으로 돌아갈 수 있습니다.

### 기타 기능
- audio manager script를 작성해 scene이 변경되어도 배경음악이 끊기지 않도록 구현했습니다.
