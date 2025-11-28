# 3DIdleGame
3D 방치형 게임 개인 프로젝트입니다.



## 1. 프로젝트 소개

'논스톱 나이트' 게임 레퍼런스 기반한 3D 방치형 게임입니다.

## 2. 프로젝트 구현 내용

1. AI Navigation을 이용, FSM 구조를 통한 AI 자동 전투 움직임 구현
2. ObjectPool 기법을 활용한 데미지 FloatingText UI 표기 
3. 인터페이스(ISkillCaster)를 이용한 다양한 Skill 구현
4. 확장 메서드(FindChild<T>)와 Resources 동적 로딩을 이용한 필드 초기화 & 동적 로딩
5. Condition 클래스를 통한 Player Stat과 (체력,골드,경험치) 통합 관리
