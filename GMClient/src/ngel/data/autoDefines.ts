export namespace Defines {

    export enum ServerStateType {
        InsideTest = 0, //내부테스트
        MainTest = 1, //메인테스트
        BetaTest = 2, //베타테스트
        Production = 3, //리얼 빌드 테스트
        QA = 4, //심의
        Review = 5, //심의
        Service = 6, //서비스
        Max,
    }

    export enum OAuthProvider {
        None = 0,
        Microsoft = 1, //마이크로소프트
        Google = 2, //구글
        Apple = 3, //애플
        Kakao = 4, //카카오
        Naver = 5, //네이버
        CustomEmail = 98, //이메일
    }

    export enum APIMethod {
        GET = 0,
        POST = 1,
    }

    export enum GameAuthType {
        Guest = 0,
        Company = 1,
        Google = 2,
        Facebook = 3,
        GameCenterIOS = 4,
    }

    export enum GMLogType {
        API = 0,
        DB = 1,
        Scheduler = 2,
        STOMP = 3,
        HubReq = 98,
        HubAck = 99,
    }

    export enum TimeZone {
        UTC = 0, //글로벌 시간
        KST = 1, //한국 표준시
        PST = 2, //태평양 표준시(PDT자동변환)
    }

    export enum AckNoticeType {
        Warning = 0, //경고
        Normal = 1, //일반
        Success = 2, //성공
    }

    export enum MailState {
        Deleted = 0,
        Arrived = 1,
        Opened = 2,
        Received = 3,
    }

    export enum MailAttribute {
        None = 0,
        System = 1,
        Clan = 2,
    }

    export enum ServiceVersionManagementPlatform {
        None = 0,
        PC = 1,
        AOS = 2,
        IOS = 3,
    }

    export enum ServiceVersionManagementServerState {
        None = 0,
        Private = 1,
        Beta = 2,
        Production = 3,
        Review = 4,
    }

    export enum ServiceVersionManagementClientState {
        None = 0,
        Service = 1,
        RecomUpdate = 2,
        Update = 3,
    }

    export enum MaintenancePlatform {
        None = 0,
        PC = 1,
        AOS = 2,
        IOS = 3,
    }

    export enum MaintenanceState {
        None = 0,
        Service = 1,
        Maintenance = 2,
    }

    export enum BlockReason {
        None = 0,
        Block = 1,
    }

    export enum ChattingNoticeType {
        None = 0,
        Fixed = 1, //고정
        LeftScroll = 2, //좌측 스크롤
    }

    export enum UserSearchType {
        None = 0, //검색 타입없음
        AccountID = 1, //UID
        Nick = 2, //닉네임
        StoveMemberNo = 3, //스토브 ID
        StoveNickNameNo = 4, //스토브 ID
    }

    export enum GameLogSearchType {
        None = 0, //검색 타입없음
        EventGroupID = 1, //이벤트 그룹 아이디
        EventID = 2, //이벤트
        SequenceNumber = 3, //시퀀스 넘버
        AccountLevel = 4, //계정 레벨
        CharacterID = 5, //캐릭터 아이디
        CharacterLevel = 6, //캐릭터 레벨
        SessionID = 7, //세션 아이디
        MarketCode = 8, //마켓
        ServerCode = 9, //서버/국가
        ChannelCode = 10, //채널
        IP = 11, //아이피
        DeviceID = 12, //기기 아이디
        DeviceType = 13, //기기 종류
        DeviceModel = 14, //기기 모델
        OS = 15, //OS
        V01 = 16, //V01
        V02 = 17, //V02
        V03 = 18, //V03
        V04 = 19, //V04
        V05 = 20, //V05
        V06 = 21, //V06
        V07 = 22, //V07
        V08 = 23, //V08
        V09 = 24, //V09
        V10 = 25, //V10
        V11 = 26, //V11
        V12 = 27, //V12
        V13 = 28, //V13
        V14 = 29, //V14
        V15 = 30, //V15
        V16 = 31, //V16
        V17 = 32, //V17
        V18 = 33, //V18
        V19 = 34, //V19
        V20 = 35, //V20
        V21 = 36, //V21
        V22 = 37, //V22
        V23 = 38, //V23
        V24 = 39, //V24
        V25 = 40, //V25
        V26 = 41, //V26
        V27 = 42, //V27
        V28 = 43, //V28
        V29 = 44, //V29
        V30 = 45, //V30
    }

    export enum UserState {
        None = 0,
        Login = 1, //Login
        OpenWorld = 2, //OpenWorld
        Rpg = 3, //Rpg
        RpgChallenge = 4, //RpgChallenge
        GoldClash = 5, //GoldClash
        Raid = 6, //Raid
    }

    export enum GameLogSortType {
        None = 0, //검색 타입없음
        LogID = 1, //로그 아이디
        EventGroupID = 2, //이벤트 그룹 아이디
        EventID = 3, //이벤트
        TimeStamp = 4, //기록 시간
        SequenceNumber = 5, //시퀀스 넘버
        StoveMember = 6, //스토브 멤버 넘버
        StoveNickname = 7, //스토브 닉네임 넘버
        AccountID = 8, //계정 아이디
        AccountLevel = 9, //계정 레벨
        AccountName = 10, //계정 이름
        CharacterID = 11, //캐릭터 아이디
        CharacterLevel = 12, //캐릭터 레벨
        SessionID = 13, //세션 아이디
        MarketCode = 14, //마켓
        ServerCode = 15, //서버/국가
        ChannelCode = 16, //채널
        IP = 17, //아이피
        DeviceID = 18, //기기 아이디
        DeviceType = 19, //기기 종류
        DeviceModel = 20, //기기 모델
        OS = 21, //OS
        V01 = 22, //V01
        V02 = 23, //V02
        V03 = 24, //V03
        V04 = 25, //V04
        V05 = 26, //V05
        V06 = 27, //V06
        V07 = 28, //V07
        V08 = 29, //V08
        V09 = 30, //V09
        V10 = 31, //V10
        V11 = 32, //V11
        V12 = 33, //V12
        V13 = 34, //V13
        V14 = 35, //V14
        V15 = 36, //V15
        V16 = 37, //V16
        V17 = 38, //V17
        V18 = 39, //V18
        V19 = 40, //V19
        V20 = 41, //V20
        V21 = 42, //V21
        V22 = 43, //V22
        V23 = 44, //V23
        V24 = 45, //V24
        V25 = 46, //V25
        V26 = 47, //V26
        V27 = 48, //V27
        V28 = 49, //V28
        V29 = 50, //V29
        V30 = 51, //V30
    }

    export enum ManageLogSortType {
        None = 0, //검색 타입없음
        UserName = 1, //유저 이름
        MethodName = 2, //기능 이름
        UrlName = 3, //url 이름
        RegTime = 4, //기록 시간
        ErrorName = 5, //오류명
        Message = 6, //메시지
    }

    export enum ChatLogSortType {
        None = 0, //검색 타입없음
        AccountId = 1, //계정 아이디
        AccountName = 2, //계정 이름
        EventID = 3, //이벤트 아이디
        StoveMemberNo = 4, //스토브 멤버 넘버
        StoveNicknameNo = 5, //스토브 닉네임 넘버
        SessionId = 6, //세션 아이디
        IpAddress = 7, //IP
        Message = 8, //메시지
        TimeStamp = 9, //일시
    }

    export enum LogSortState {
        None = 0, //미정렬
        ASC = 1, //오름차순
        Desc = 2, //내림차순
    }

    export enum GameType {
        None = 0,
        GoldClash = 1, //골드클래시
        Felling = 2, //나무베기
        OXQuiz = 3, //OX퀴즈
        VehicleRace = 4, //탈것 경주
        DiceGame = 5, //주사위 게임
    }

    export enum FirewallMethod {
        Allow = 0, //허용
        Deny = 1, //제한
    }

    export enum UserJobType {
        None = 0, //없음
        SendImmdGameMail = 1, //우편 즉시발송
        InsertEventMail = 2, //이벤트 메일 추가
        UpdateEventMail = 3, //이벤트 메일 수정
        RemoveEventMail = 4, //이벤트 메일 삭제
    }

    export enum SettingType {
        None = 0, //없음
        ExpireLog = 1, //운영툴 로그 말료 일수
    }

    export enum ChattingMessageType {
        Normal = 0, //일반
        Notice = 1, //공지
        Whisper = 2, //귓속말
        Reply = 3, //답글
    }

}
