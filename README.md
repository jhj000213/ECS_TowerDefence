# ECS_TowerDefence
For Edu ECS, DOTS


2026-04-21 ~

목표 : ECS로 타워디펜스를 만들자. 
조건 : 
- 객체를 매우 많이 사용하면서 프레임 드랍과 발열을 최소화하자
- 하이브리드 ECS, 더 나아가 Pure ECS를 습득하고 체화하자.

개발 계획
- DOTS를 처음 공부하는 만큼, 일단 개념을 확실히 잡고 갈 것. CD(Component Data), Baker, System, JobEntity 등의 역할을 이해하고 확실히 분리할 것
- 하이브리드 ECS로 개발 (엔티티로 연산하고, 기존의 애니메이션 객체들이 이를 따라가는 방식)
유닛갯수를 계속 늘리며 한계점 체크
- VAT 및 shader를 공부해보며 Pure ECS로 전환
