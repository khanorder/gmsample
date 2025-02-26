    export enum Errors {
        None = 0,
        Unknown = 1,

        Common_Forbidden, //2 : 접근금지(권한없음)
        Common_NotFoundToken, //3 : 인증 토큰없음
        Common_FailedToEncodeMsgPack, //4 : 데이터를 메세지팩으로 변환 실패
        Common_FailedToDecodeMsgPack, //5 : 메세지팩을 데이터로 변환 실패
        Common_PacketArgsEmpty, //6 : 패킷인자 없음
        Common_PacketNull, //7 : 패킷 없음
        Common_FailedToParse, //8 : 변환 실패
        Common_Denied, //9 : 방화벽 제제

        DefaultWebSocketAuthorize_HTTPContextNull, //10 : HTTP 객체 없음
        DefaultWebSocketAuthorize_Forbidden, //11 : 로그인 정보없음
        DefaultWebSocketAuthorize_NotFoundSignInId, //12 : 로그인 고유 아이디 없음
        DefaultWebSocketAuthorize_NotFoundSignInUser, //13 : 로그인 정보 유저정보 없음
        DefaultWebSocketAuthorize_AuthExpired, //14 : 로그인 정보 만료

        DownloadFile_EmptyData, //15 : 데이터 없음
        DownloadFile_EmptyFileName, //16 : 파일명 없음
        DownloadFile_EmptyContentType, //17 : 파일형식 없음

        DefaultAPIAuthorize_Forbidden, //18 : 로그인 정보없음
        DefaultAPIAuthorize_NotFoundSignInId, //19 : 로그인 고유 아이디 없음
        DefaultAPIAuthorize_NotFoundSignInUser, //20 : 로그인 정보 유저정보 없음
        DefaultAPIAuthorize_AuthExpired, //21 : 로그인 정보 만료

        IssueTokenkey_UserRequired, //22 : 사용자 정보 필요
        IssueTokenkey_ClientIdRequired, //23 : 클라이언트 아이디 필요
        IssueTokenkey_NotFoundClientInfo, //24 : 클라이언트 정보 없음
        IssueTokenkey_FailedToIssueToken, //25 : 토큰발행 실패
        IssueTokenkey_UnknownError, //26 : 알 수없는 오류

        SignInClaim_HTTPContextNull, //27 : HTTP 객체 없음
        SignInClaim_ClientIdRequired, //28 : 클라이언트 아이디 없음
        SignInClaim_NotFoundClientId, //29 : 등록되지 않은 클라이언트 아이디
        SignInClaim_FailedToInsertSuperRole, //30 : 최고권한 생성 실패
        SignInClaim_FailedToAddSuperRoleUser, //31 : 유저에게 최고권한 부여 실패
        SignInClaim_AlreadySignIn, //32 : 이미 로그인 됨
        SignInClaim_FailedToIssueToken, //33 : 인증토큰 생성 실패
        SignInClaim_UnknownError, //34 : 예외

        JWTDeserializeToken_TokenStringRequired, //35 : 토큰 내용이 필요
        JWTDeserializeToken_FailedToReadToken, //36 : 토큰내용 확인 실패
        JWTDeserializeToken_TokenNull, //37 : 토큰내용 없음
        JWTDeserializeToken_AudienceRequired, //38 : 토큰 수신자 정보 필요
        JWTDeserializeToken_TokenExpired, //39 : 만료된 토근
        JWTDeserializeToken_TokenSigningKeyRequired, //40 : 토근 서명 필요
        JWTDeserializeToken_FailedToValidateToken, //41 : 유효하지 않은 토큰
        JWTDeserializeToken_ValidTokenNull, //42 : 유효한 토큰 없음

        JWTGetPrincipal_TokenStringRequired, //43 : 토큰 내용이 필요
        JWTGetPrincipal_FailedToReadToken, //44 : 토큰내용 확인 실패
        JWTGetPrincipal_TokenNull, //45 : 토큰내용 없음
        JWTGetPrincipal_AudienceRequired, //46 : 토큰 수신자 정보 필요
        JWTGetPrincipal_TokenExpired, //47 : 만료된 토근
        JWTGetPrincipal_TokenSigningKeyRequired, //48 : 토근 서명 필요
        JWTGetPrincipal_FailedToValidateToken, //49 : 유효하지 않은 토큰
        JWTGetPrincipal_ValidTokenNull, //50 : 유효한 토큰 없음

        Authentication_ClientIdRequired, //51 : 클라이언트 아이디 없음
        Authentication_NotFoundClientId, //52 : 등록되지 않은 클라이언트 아이디
        Authentication_OAuthProviderRequired, //53 : 인증형식 값 필요
        Authentication_OidRequired, //54 : OAuth 고유 아이디 필요
        Authentication_EmailRequired, //55 : 이메일 정보 필요
        Authentication_NameRequired, //56 : 이름 필요
        Authentication_ConnectionIdRequired, //57 : 허브 연결 아이디 필요
        Authentication_FailedSignUp, //58 : 인증 후 가입 실패
        Authentication_NotFoundUser, //59 : 사용자 정보 없음
        Authentication_DeletedUser, //60 : 삭제된 유저
        Authentication_AlreadySignIn, //61 : 이미 로그인됨
        Authentication_FailedToInsertSignInTime, //62 : 로그인 시간 입력 실패
        Authentication_FailedToUpdateSignInTime, //63 : 로그인 시간 업데이트 실패
        Authentication_FailedSignIn, //64 : 인증 실패
        Authentication_UnknownError, //65 : 예외

        NaverGetUser_AccessTokenRequired, //66 : 접근 토큰 필요
        NaverGetUser_FailedToRequestProfileAPI, //67 : 프로필 API 요청실패
        NaverGetUser_NotFoundUser, //68 : 유저 정보없음

        KakaoGetUser_AccessTokenRequired, //69 : 접근 토큰 필요
        KakaoGetUser_FailedToRequestProfileAPI, //70 : 프로필 API 요청실패
        KakaoGetUser_NotFoundUser, //71 : 유저 정보없음

        Connected_HTTPContextNull, //72 : HTTP 객체 없음
        Connected_NotFoundAuth, //73 : 로그인 정보 없음
        Connected_NotFoundSigninId, //74 : 로그인 아이디 없음
        Connected_SigninIdMustGuid, //75 : 로그인 아이디는 Guid 형식
        Connected_NotFoundOAuthKeyId, //76 : OAuth Key 아이디 없음
        Connected_OAuthKeyIdMustGuid, //77 : OAuth Key 아이디 Guid 형식
        Connected_AuthRemoved, //78 : 삭제된 인증
        Connected_AuthExpired, //79 : 인증시간 만료
        Connected_UnknownError, //80 : 예외

        SignIn_OAuthError, //81 : OAuth 인증오류
        SignIn_ClientIdRequired, //82 : 클라이언트 아이디 필요
        SignIn_NotAllowedOAuthProvider, //83 : 허용된 인증형식이 아님
        SignIn_OAuthCodeRequired, //84 : 인증코드 필요
        SignIn_OAuthStateCodeRequired, //85 : 인증상태코드 필요
        SignIn_FailedToGetUser, //86 : 인증실패
        SignIn_ClientIdMustGuid, //87 : 클라이언트 아이디는 Guid 형식
        SignIn_NotAllowedClientId, //88 : 허가된 클라이언트 아이디 아님
        SignIn_AlreadySignIn, //89 : 이미 로그인됨
        SignIn_TokenRequired, //90 : 인증토큰 필요
        SignIn_FailedToParseToken, //91 : 토큰 변환실패
        SignIn_EmailRequired, //92 : 이메일 정보 필요
        SignIn_MustUseCompanyMail, //93 : 회사 메일이 아님
        SignIn_NameRequired, //94 : 이름 필요
        SignIn_OidRequired, //95 : OAuth 고유 아이디 필요
        SignIn_FailedSignIn, //96 : 인증 실패

        SignInLDAP_ClientIdRequired, //97 : 클라이언트 아이디 필요
        SignInLDAP_ClientIdMustGuid, //98 : 클라이언트 아이디 형식 오류
        SignInLDAP_NotAllowedClientId, //99 : 허가된 클라이언트 아이디 아님
        SignInLDAP_AlreadySignIn, //100 : 이미 로그인됨
        SignInLDAP_EmailRequired, //101 : 이메일 입력 필요
        SignInLDAP_MustUseCompanyMail, //102 : 회사 메일이 아님
        SignInLDAP_PasswordRequired, //103 : 비밀번호 입력 필요
        SignInLDAP_DoNotMatchedData, //104 : 이메일 또는 비밀번호 불일치
        SignInLDAP_OAuthEmailNull, //105 : 인증된 이메일 정보 없음
        SignInLDAP_OAuthNameNull, //106 : 인증된 이름 정보 없음
        SignInLDAP_OAuthUidNull, //107 : 인증된 고유번호 정보 없음
        SignInLDAP_FailedSignIn, //108 : 인증 실패
        SignInLDAP_CanNotTrySignIn, //109 : 인증시도 실패

        UpdateSignIn_ReissueToken, //110 : 토큰 갱신
        UpdateSignIn_HTTPContextNull, //111 : HTTP 객체 없음
        UpdateSignIn_TokenEmpty, //112 : 인증토큰 없음
        UpdateSignIn_TokenNull, //113 : 인증토큰 변환실패
        UpdateSignIn_ConnectionIdRequired, //114 : 연결 아이디 필요
        UpdateSignIn_TokenExpired, //115 : 인증토큰 만료
        UpdateSignIn_NotFoundAuth, //116 : 로그인 정보 없음
        UpdateSignIn_NotFoundSigninId, //117 : 로그인 아이디 없음
        UpdateSignIn_SigninIdMustGuid, //118 : 로그인 아이디는 Guid 형식
        UpdateSignIn_NotFoundOAuthKeyId, //119 : OAuth Key 아이디 없음
        UpdateSignIn_OAuthKeyIdMustGuid, //120 : OAuth Key 아이디 Guid 형식
        UpdateSignIn_AudienceRequired, //121 : 토큰 수신자 정보 필요
        UpdateSignIn_NotFoundUser, //122 : 유저정보 없음
        UpdateSignIn_DeletedUser, //123 : 삭제된 유저
        UpdateSignIn_NotSignedInUser, //124 : 로그인하지 않은 유저
        UpdateSignIn_AlreadySignIn, //125 : 이미 로그인됨
        UpdateSignIn_AuthExpired, //126 : 인증시간 만료
        UpdateSignIn_UnknownError, //127 : 예외
        UpdateSignIn_ClientIdRequired, //128 : 클라이언트 아이디 필요
        UpdateSignIn_ClientIdMustGuid, //129 : 클라이언트 아이디는 Guid 형식
        UpdateSignIn_NotAllowedClientId, //130 : 허가된 클라이언트 아이디 아님
        UpdateSignIn_NotFoundClientInfo, //131 : 클라이언트 정보 없음
        UpdateSignIn_FailedToIssueToken, //132 : 인증토큰 생성 실패
        UpdateSignIn_Forbidden, //133 : 로그인 정보없음
        UpdateSignIn_SignInIdRquired, //134 : 사인아이디 필요
        UpdateSignIn_OAuthKeyIdRquired, //135 : 인증 아이디 필요
        UpdateSignIn_UpdateTimeRquired, //136 : 업데이트 시간 필요
        UpdateSignIn_UpdateTimeMustTimeStamp, //137 : 업데이트 시간은 시간형식
        UpdateSignIn_FailedToInsertSignInTime, //138 : 로그인 시간 입력 실패
        UpdateSignIn_FailedToUpdateSignInTime, //139 : 로그인 시간 입력 실패

        SignOut_TokenEmpty, //140 : 인증토큰 없음
        SignOut_TokenNull, //141 : 인증토큰 변환실패
        SignOut_NotFoundUserId, //142 : 사용자 아이디 없음
        SignOut_UserIdMustGuid, //143 : 사용자 아이디는 Guid 형식
        SignOut_HTTPContextNull, //144 : HTTP 객체 없음
        SignOut_NotFoundAuth, //145 : 로그인 정보 없음
        SignOut_UnknownError, //146 : 예외

        GetSignedInUser_TokenEmpty, //147 : 인증토큰 없음
        GetSignedInUser_TokenNull, //148 : 인증토큰 변환실패
        GetSignedInUser_TokenExpired, //149 : 인증토큰 만료
        GetSignedInUser_HTTPContextNull, //150 : HTTP 객체 없음
        GetSignedInUser_NotFoundAuth, //151 : 로그인 정보 없음
        GetSignedInUser_NotFoundSigninId, //152 : 로그인 아이디 없음
        GetSignedInUser_SigninIdMustGuid, //153 : 로그인 아이디는 Guid 형식
        GetSignedInUser_NotFoundOAuthKeyId, //154 : OAuth Key 아이디 없음
        GetSignedInUser_OAuthKeyIdMustGuid, //155 : OAuth Key 아이디 Guid 형식
        GetSignedInUser_NotFoundUser, //156 : 사용자 정보 없음
        GetSignedInUser_DeletedUser, //157 : 삭제된 유저
        GetSignedInUser_NotFoundUserSignin, //158 : 로그인 정보 없음
        GetSignedInUser_AuthExpired, //159 : 만료된 로그인 정보
        GetSignedInUser_UnknownError, //160 : 예외

        CheckAuthentication_TokenRequired, //161 : 토큰 필요
        CheckAuthentication_ReissueToken, //162 : 토큰 갱신
        CheckAuthentication_HTTPContextNull, //163 : HTTP 객체 없음
        CheckAuthentication_NotFoundAuth, //164 : 로그인 정보 없음
        CheckAuthentication_NotFoundSigninId, //165 : 로그인 아이디 없음
        CheckAuthentication_AuthExpired, //166 : 인증시간 만료
        CheckAuthentication_NotFoundUser, //167 : 유저 정보 없음
        CheckAuthentication_UnknownError, //168 : 예외
        CheckAuthentication_FailedToReissueToken, //169 : 토큰 재발행 실패

        CheckConnection_ReissueToken, //170 : 토큰 갱신
        CheckConnection_HTTPContextNull, //171 : HTTP 객체 없음
        CheckConnection_NotFoundAuth, //172 : 로그인 정보 없음
        CheckConnection_NotFoundSignInId, //173 : 로그인 아이디 없음
        CheckConnection_SigninIdMustGuid, //174 : 로그인 아이디는 Guid 형식
        CheckConnection_NotFoundOAuthKeyId, //175 : OAuth Key 아이디 없음
        CheckConnection_OAuthKeyIdMustGuid, //176 : OAuth Key 아이디 Guid 형식
        CheckConnection_FailedToRefreshSignIn, //177 : OAuth Key ID 없음
        CheckConnection_NotFoundUser, //178 : 유저정보 없음
        CheckConnection_AuthExpired, //179 : 로그인 정보 만료
        CheckConnection_PasswordExpired, //180 : 비밀번호 만료
        CheckConnection_FailedToReissueToken, //181 : 토큰 재발행 실패

        KickUser_UIDRequired, //182 : 계정 고유번호 필요
        KickUser_UIDMustInt64, //183 : 유저정보 없음
        KickUser_NotFoundUserAccount, //184 : 유저정보 없음
        KickUser_NotSignedInUser, //185 : 유저정보 없음

        Account_UIDRequired, //186 : 계정 고유번호 필요
        Account_NotFoundData, //187 : 데이터 없음

        Achievement_UIDRequired, //188 : 계정 고유번호 필요
        Achievement_NotFoundData, //189 : 데이터 없음

        SaveAchievements_EmptyData, //190 : 저장할 데이터 없음
        SaveAchievements_UIDRequired, //191 : 계정 고유번호 필요
        SaveAchievements_AchievementIDRequired, //192 : 업적 아이디 필요
        SaveAchievements_NotSuitableAchievement, //193 : 부적합한 업적 정보 없음
        SaveAchievements_CountOneOrMore, //194 : 업접 수행횟수는 1이상
        SaveAchievements_CountOverMax, //195 : 업접 수행횟수 최대치
        SaveAchievements_CompleteAtRequired, //196 : 완료시간 필요
        SaveAchievements_NotFoundUserAccount, //197 : 대상 사용자 정보없음
        SaveAchievements_FailedToInsert, //198 : 테이터 추가 실패
        SaveAchievements_FailedToUpdate, //199 : 테이터 추가 실패

        DeleteAchievements_EmptyData, //200 : 저장할 데이터 없음
        DeleteAchievements_UIDRequired, //201 : 계정 고유번호 필요
        DeleteAchievements_AchievementIDRequired, //202 : 업적 아이디 필요
        DeleteAchievements_NotFoundUserAccount, //203 : 대상 사용자 정보없음
        DeleteAchievements_NotFoundAnyData, //204 : 데이터 없음
        DeleteAchievements_NotFoundData, //205 : 삭제할 데이터 없음
        DeleteAchievements_FailedToDelete, //206 : 테이터 삭제 실패

        Artifact_UIDRequired, //207 : 계정 고유번호 필요
        Artifact_NotFoundData, //208 : 데이터 없음

        SaveArtifacts_EmptyData, //209 : 저장할 데이터 없음
        SaveArtifacts_UIDRequired, //210 : 계정 고유번호 필요
        SaveArtifacts_ArtifactIDRequired, //211 : 유물 아이디 필요
        SaveArtifacts_CountZeroOrMore, //212 : 수량은 0이상
        SaveArtifacts_NotFoundUserAccount, //213 : 대상 사용자 정보없음
        SaveArtifacts_FailedToInsert, //214 : 테이터 추가 실패
        SaveArtifacts_FailedToUpdate, //215 : 테이터 추가 실패

        DeleteArtifacts_EmptyData, //216 : 저장할 데이터 없음
        DeleteArtifacts_UIDRequired, //217 : 계정 고유번호 필요
        DeleteArtifacts_ArtifactIDRequired, //218 : 유물 아이디 필요
        DeleteArtifacts_NotFoundUserAccount, //219 : 대상 사용자 정보없음
        DeleteArtifacts_NotFoundAnyData, //220 : 데이터 없음
        DeleteArtifacts_NotFoundData, //221 : 삭제할 데이터 없음
        DeleteArtifacts_FailedToDelete, //222 : 테이터 삭제 실패

        ArtifactDeck_UIDRequired, //223 : 계정 고유번호 필요
        ArtifactDeck_NotFoundData, //224 : 데이터 없음

        Assets_UIDRequired, //225 : 계정 고유번호 필요
        Assets_NotFoundData, //226 : 데이터 없음

        SaveAssets_EmptyData, //227 : 저장할 데이터 없음
        SaveAssets_UIDRequired, //228 : 계정 고유번호 필요
        SaveAssets_AssetIDRequired, //229 : 재화 아이디 필요
        SaveAssets_CountZeroOrMore, //230 : 수량은 0이상
        SaveAssets_NotFoundUserAccount, //231 : 대상 사용자 정보없음
        SaveAssets_FailedToInsert, //232 : 테이터 추가 실패
        SaveAssets_FailedToUpdate, //233 : 테이터 추가 실패

        DeleteAssets_EmptyData, //234 : 저장할 데이터 없음
        DeleteAssets_UIDRequired, //235 : 계정 고유번호 필요
        DeleteAssets_AssetIDRequired, //236 : 재화 아이디 필요
        DeleteAssets_NotFoundUserAccount, //237 : 대상 사용자 정보없음
        DeleteAssets_NotFoundAnyData, //238 : 데이터 없음
        DeleteAssets_NotFoundData, //239 : 삭제할 데이터 없음
        DeleteAssets_FailedToDelete, //240 : 테이터 삭제 실패

        Attendance_UIDRequired, //241 : 계정 고유번호 필요
        Attendance_NotFoundData, //242 : 데이터 없음

        SaveAttendance_UIDRequired, //243 : 계정 고유번호 필요
        SaveAttendance_AttendanceIDRequired, //244 : 출석체크 아이디 필요
        SaveAttendance_RewardStateRequired, //245 : 출석체크 보상수령 상태 필요
        SaveAttendance_NotFoundSuitableAttendanceData, //246 : 적합한 출석체크 정보가 없습니다
        SaveAttendance_NotFoundSuitableRewardState, //247 : 출석보상 정보의 갯수가 적합하지 않습니다
        SaveAttendance_NotFoundUserAccount, //248 : 대상 사용자 정보없음
        SaveAttendance_FailedToInsert, //249 : 보상수령 정보 입력 실패
        SaveAttendance_FailedToUpdate, //250 : 보상수령 정보 수정 실패

        Collection_UIDRequired, //251 : 계정 고유번호 필요
        Collection_NotFoundData, //252 : 데이터 없음

        SaveCollections_EmptyData, //253 : 저장할 데이터 없음
        SaveCollections_UIDRequired, //254 : 계정 고유번호 필요
        SaveCollections_NotSuitableCollectionType, //255 : 부적합한 도감 타입
        SaveCollections_CollectionIDRequired, //256 : 도감 아이디 필요
        SaveCollections_NotFoundUserAccount, //257 : 대상 사용자 정보없음
        SaveCollections_FailedToSave, //258 : 테이터 저장 실패

        DeleteCollections_EmptyData, //259 : 저장할 데이터 없음
        DeleteCollections_UIDRequired, //260 : 계정 고유번호 필요
        DeleteCollections_NotSuitableCollectionType, //261 : 부적합한 도감 타입
        DeleteCollections_CollectionIDRequired, //262 : 도감 아이디 필요
        DeleteCollections_NotFoundUserAccount, //263 : 대상 사용자 정보없음
        DeleteCollections_NotFoundAnyData, //264 : 데이터 없음
        DeleteCollections_NotFoundData, //265 : 삭제할 데이터 없음
        DeleteCollections_FailedToDelete, //266 : 테이터 삭제 실패

        Craft_UIDRequired, //267 : 계정 고유번호 필요
        Craft_NotFoundData, //268 : 데이터 없음

        Entitlement_UIDRequired, //269 : 계정 고유번호 필요
        Entitlement_NotFoundData, //270 : 데이터 없음

        SaveEntitlements_EmptyData, //271 : 저장할 데이터 없음
        SaveEntitlements_UIDRequired, //272 : 계정 고유번호 필요
        SaveEntitlements_EntitlementIDRequired, //273 : 칭호 아이디 필요
        SaveEntitlements_NotFoundUserAccount, //274 : 대상 사용자 정보없음
        SaveEntitlements_FailedToInsert, //275 : 테이터 추가 실패
        SaveEntitlements_AlreadyExistsData, //276 : 같은 테이터 존재

        DeleteEntitlements_EmptyData, //277 : 저장할 데이터 없음
        DeleteEntitlements_UIDRequired, //278 : 계정 고유번호 필요
        DeleteEntitlements_EntitlementIDRequired, //279 : 칭호 아이디 필요
        DeleteEntitlements_NotFoundUserAccount, //280 : 대상 사용자 정보없음
        DeleteEntitlements_NotFoundAnyData, //281 : 데이터 없음
        DeleteEntitlements_NotFoundData, //282 : 삭제할 데이터 없음
        DeleteEntitlements_FailedToDelete, //283 : 테이터 삭제 실패

        Friend_UIDRequired, //284 : UID 필요
        Friend_NotFoundData, //285 : 데이터 없음

        GlitchStore_UIDRequired, //286 : UID 필요
        GlitchStore_NotFoundData, //287 : 데이터 없음

        GuideMission_UIDRequired, //288 : 계정 고유번호 필요
        GuideMission_NotFoundData, //289 : 데이터 없음

        SaveGuideMissions_EmptyData, //290 : 저장할 데이터 없음
        SaveGuideMissions_UIDRequired, //291 : 계정 고유번호 필요
        SaveGuideMissions_GuideMissionCategoryRequired, //292 : 가이드 미션 카테고리 필요
        SaveGuideMissions_MissionIDRequired, //293 : 가이드 미션 아이디 필요
        SaveGuideMissions_NotSuitableGuideMissionData, //294 : 부적합한 미션 데이터
        SaveGuideMissions_NotFoundUserAccount, //295 : 대상 사용자 정보없음
        SaveGuideMissions_NotFoundData, //296 : 시즌미션 정보없음
        SaveGuideMissions_FailedToUpdate, //297 : 테이터 추가 실패

        GuideMissionProgressReward_UIDRequired, //298 : 계정 고유번호 필요
        GuideMissionProgressReward_NotFoundData, //299 : 데이터 없음

        Hero_UIDRequired, //300 : 계정 고유번호 필요
        Hero_NotFoundData, //301 : 데이터 없음

        HeroSkin_UIDRequired, //302 : 계정 고유번호 필요
        HeroSkin_NotFoundData, //303 : 데이터 없음

        HeroSkinPreset_UIDRequired, //304 : 계정 고유번호 필요
        HeroSkinPreset_NotFoundData, //305 : 데이터 없음

        Inventory_UIDRequired, //306 : 계정 고유번호 필요
        Inventory_NotFoundData, //307 : 데이터 없음

        SaveInventories_EmptyData, //308 : 저장할 데이터 없음
        SaveInventories_UIDRequired, //309 : 계정 고유번호 필요
        SaveInventories_ItemIDRequired, //310 : 아이템 아이디 필요
        SaveInventories_CountZeroOrMore, //311 : 수량은 0이상
        SaveInventories_NotFoundUserAccount, //312 : 대상 사용자 정보없음
        SaveInventories_FailedToInsert, //313 : 테이터 추가 실패
        SaveInventories_FailedToUpdate, //314 : 테이터 추가 실패

        DeleteInventories_EmptyData, //315 : 저장할 데이터 없음
        DeleteInventories_UIDRequired, //316 : 계정 고유번호 필요
        DeleteInventories_ItemIDRequired, //317 : 아이템 아이디 필요
        DeleteInventories_NotFoundUserAccount, //318 : 대상 사용자 정보없음
        DeleteInventories_NotFoundAnyData, //319 : 데이터 없음
        DeleteInventories_NotFoundData, //320 : 삭제할 데이터 없음
        DeleteInventories_FailedToDelete, //321 : 테이터 삭제 실패

        Mail_UIDRequired, //322 : 계정 고유번호 필요
        Mail_NotFoundData, //323 : 데이터 없음

        MazeRewardBox_UIDRequired, //324 : 계정 고유번호 필요
        MazeRewardBox_NotFoundData, //325 : 데이터 없음

        NicePlayers_UIDRequired, //326 : 계정 고유번호 필요
        NicePlayers_NotFoundData, //327 : 칭찬 정보 없음

        Penalties_UIDRequired, //328 : 계정 고유번호 필요
        Penalties_StartTimeRequired, //329 : 시작일시 필요
        Penalties_StartTimeNotSuitableDateTime, //330 : 시작일시 날짜형식 안맞음
        Penalties_EndTimeRequired, //331 : 종료일시 필요
        Penalties_EndTimeNotSuitableDateTime, //332 : 시작일시 날짜형식 안맞음
        Penalties_SearchPeriodLimit, //333 : 검색기간 넘음
        Penalties_NotFoundData, //334 : 패널티 정보 없음

        Penalty_EmptyParameters, //335 : 인자 없음
        Penalty_UIDRequired, //336 : 계정 고유번호 필요
        Penalty_PenaltyReportStateRequired, //337 : 패널티 형태 필요
        Penalty_NotFoundData, //338 : 패널티 정보 없음

        SavePenalty_EmptyData, //339 : 저장할 데이터 없음
        SavePenalty_UIDRequired, //340 : 계정 고유번호 필요
        SavePenalty_PenaltyReportStateRequired, //341 : 패널티 형식 필요
        SavePenalty_NotSuitablePenaltyReportState, //342 : 적합한 패널티 형식 아님
        SavePenalty_PenaltyGradeReqired, //343 : 패널티 등급 필요
        SavePenalty_NotSuitablePenaltyGrade, //344 : 적합한 패널티 등급 아님
        SavePenalty_NotAllowUpperPenaltyGrade, //345 : 패널티 등급 상향 불가
        SavePenalty_NotFoundUserAccount, //346 : 대상 사용자 정보없음
        SavePenalty_NotFoundData, //347 : 패널티 정보없음
        SavePenalty_FailedToUpdate, //348 : 테이터 수정 실패

        Pet_UIDRequired, //349 : 계정 고유번호 필요
        Pet_NotFoundData, //350 : 데이터 없음

        PlayRecordGoldClashes_UIDRequired, //351 : 계정 고유번호 필요
        PlayRecordGoldClashes_NotFoundData, //352 : 데이터 없음

        PlayRecordRpgs_UIDRequired, //353 : 계정 고유번호 필요
        PlayRecordRpgs_NotFoundData, //354 : 데이터 없음

        Production_UIDRequired, //355 : 계정 고유번호 필요
        Production_NotFoundData, //356 : 데이터 없음

        SaveProductions_EmptyData, //357 : 저장할 데이터 없음
        SaveProductions_UIDRequired, //358 : 계정 고유번호 필요
        SaveProductions_ProductionTypeRequired, //359 : 생산시설 형태 필요
        SaveProductions_ProductionLevelRequired, //360 : 생산시설 레벨 필요
        SaveProductions_ProductionStartAtRequired, //361 : 생산 시작시간 필요
        SaveProductions_NotFoundUserAccount, //362 : 대상 사용자 정보없음
        SaveProductions_NotFoundData, //363 : 생산시설 정보없음
        SaveProductions_FailedToUpdate, //364 : 테이터 수정 실패

        Profile_UIDRequired, //365 : 계정 고유번호 필요
        Profile_NotFoundData, //366 : 데이터 없음

        SaveProfiles_EmptyData, //367 : 저장할 데이터 없음
        SaveProfiles_UIDRequired, //368 : 계정 고유번호 필요
        SaveProfiles_ProfileIDRequired, //369 : 프로필 아이디 필요
        SaveProfiles_ProfileTypeRequired, //370 : 프로필 형식 필요
        SaveProfiles_NotFoundUserAccount, //371 : 대상 사용자 정보없음
        SaveProfiles_AlreadyExistsData, //372 : 이미 있는 프로필
        SaveProfiles_FailedToInsert, //373 : 테이터 추가 실패
        SaveProfiles_FailedToUpdate, //374 : 테이터 추가 실패

        DeleteProfiles_EmptyData, //375 : 저장할 데이터 없음
        DeleteProfiles_UIDRequired, //376 : 계정 고유번호 필요
        DeleteProfiles_ProfileIDRequired, //377 : 프로필 아이디 필요
        DeleteProfiles_NotFoundUserAccount, //378 : 대상 사용자 정보없음
        DeleteProfiles_NotFoundAnyData, //379 : 데이터 없음
        DeleteProfiles_NotFoundData, //380 : 삭제할 데이터 없음
        DeleteProfiles_FailedToDelete, //381 : 테이터 삭제 실패

        RankingReward_UIDRequired, //382 : UID 필요
        RankingReward_NotFoundData, //383 : 데이터 없음

        RpgAttribute_UIDRequired, //384 : 계정 고유번호 필요
        RpgAttribute_NotFoundData, //385 : 데이터 없음

        SaveRpgAttributes_EmptyData, //386 : 저장할 데이터 없음
        SaveRpgAttributes_UIDRequired, //387 : 계정 고유번호 필요
        SaveRpgAttributes_IDRequired, //388 : 특성 아이디 필요
        SaveRpgAttributes_LevelOneOrMore, //389 : 레벨은 1이상
        SaveRpgAttributes_NotFoundUserAccount, //390 : 대상 사용자 정보없음
        SaveRpgAttributes_FailedToInsert, //391 : 테이터 추가 실패
        SaveRpgAttributes_FailedToUpdate, //392 : 테이터 추가 실패

        DeleteRpgAttributes_EmptyData, //393 : 저장할 데이터 없음
        DeleteRpgAttributes_UIDRequired, //394 : 계정 고유번호 필요
        DeleteRpgAttributes_IDRequired, //395 : 특성 아이디 필요
        DeleteRpgAttributes_NotFoundUserAccount, //396 : 대상 사용자 정보없음
        DeleteRpgAttributes_NotFoundAnyData, //397 : 데이터 없음
        DeleteRpgAttributes_NotFoundData, //398 : 삭제할 데이터 없음
        DeleteRpgAttributes_FailedToDelete, //399 : 테이터 삭제 실패

        SeasonMission_UIDRequired, //400 : 계정 고유번호 필요
        SeasonMission_NotFoundData, //401 : 데이터 없음

        SaveSeasonMissions_EmptyData, //402 : 저장할 데이터 없음
        SaveSeasonMissions_UIDRequired, //403 : 계정 고유번호 필요
        SaveSeasonMissions_SeasonPassIDRequired, //404 : 시즌패스 아이디 필요
        SaveSeasonMissions_MissionIDRequired, //405 : 미션 아이디 필요
        SaveSeasonMissions_NotSuitableSeasonMissionData, //406 : 부적합한 미션 데이터
        SaveSeasonMissions_CountZeroOrMore, //407 : 횟수는 0이상
        SaveSeasonMissions_NotSuitableSeasonMissionCount, //408 : 부적합한 미션 횟수
        SaveSeasonMissions_NotFoundUserAccount, //409 : 대상 사용자 정보없음
        SaveSeasonMissions_NotFoundData, //410 : 시즌미션 정보없음
        SaveSeasonMissions_FailedToUpdate, //411 : 테이터 추가 실패

        SeasonPass_UIDRequired, //412 : 계정 고유번호 필요
        SeasonPass_NotFoundData, //413 : 데이터 없음

        SaveSeasonPass_EmptyData, //414 : 저장할 데이터 없음
        SaveSeasonPass_UIDRequired, //415 : 계정 고유번호 필요
        SaveSeasonPass_SeasonPassIDRequired, //416 : 시즌패스 아이디 필요
        SaveSeasonPass_NotFoundSuitableSeasonPassData, //417 : 적합한 시즌패스 정보가 없습니다
        SaveSeasonPass_SeasonNumRequired, //418 : 시즌 넘버 필요
        SaveSeasonPass_LevelRequired, //419 : 레벨 필요
        SaveSeasonPass_ExpRequired, //420 : 경험치 필요
        SaveSeasonPass_RewardStateRequired, //421 : 시즌패스 보상수령 상태 필요
        SaveSeasonPass_NotFoundSuitableRewardState, //422 : 시즌패스 정보의 갯수가 적합하지 않습니다
        SaveSeasonPass_NotFoundUserAccount, //423 : 대상 사용자 정보없음
        SaveSeasonPass_FailedToInsert, //424 : 보상수령 정보 입력 실패
        SaveSeasonPass_FailedToUpdate, //425 : 보상수령 정보 수정 실패

        Trade_UIDRequired, //426 : 계정 고유번호 필요
        Trade_NotFoundData, //427 : 데이터 없음

        TreasureBox_UIDRequired, //428 : 계정 고유번호 필요
        TreasureBox_NotFoundData, //429 : 데이터 없음

        SaveTreasureBoxes_EmptyData, //430 : 저장할 데이터 없음
        SaveTreasureBoxes_UIDRequired, //431 : 계정 고유번호 필요
        SaveTreasureBoxes_BoxIDRequired, //432 : 보물상자 아이디 필요
        SaveTreasureBoxes_NotSuitableBoxID, //433 : 부적합한 보물상자 정보 없음
        SaveTreasureBoxes_OpenAtRequired, //434 : 상자 획득시간 필요
        SaveTreasureBoxes_NotFoundUserAccount, //435 : 대상 사용자 정보없음
        SaveTreasureBoxes_AlreadyExistsData, //436 : 이미 획득한 보물상자
        SaveTreasureBoxes_FailedToInsert, //437 : 테이터 추가 실패
        SaveTreasureBoxes_FailedToUpdate, //438 : 테이터 추가 실패

        DeleteTreasureBoxes_EmptyData, //439 : 저장할 데이터 없음
        DeleteTreasureBoxes_UIDRequired, //440 : 계정 고유번호 필요
        DeleteTreasureBoxes_BoxIDRequired, //441 : 보물상자 아이디 필요
        DeleteTreasureBoxes_NotFoundUserAccount, //442 : 대상 사용자 정보없음
        DeleteTreasureBoxes_NotFoundAnyData, //443 : 데이터 없음
        DeleteTreasureBoxes_NotFoundData, //444 : 삭제할 데이터 없음
        DeleteTreasureBoxes_FailedToDelete, //445 : 테이터 삭제 실패

        UserAccountByNick_NickRequired, //446 : 닉네임 입력 필요
        UserAccountByNick_NotFoundUserAccount, //447 : 유저정보 없음

        UserAccountByUID_UIDRequired, //448 : UID 입력 필요
        UserAccountByUID_UIDMustInt64, //449 : UID는 정수
        UserAccountByUID_NotFoundUserAccount, //450 : 유저정보 없음

        UserAccountByMemberNo_MemberNoRequired, //451 : MemberNo 입력 필요
        UserAccountByMemberNo_MemberNoMustUInt64, //452 : MemberNo는 정수
        UserAccountByMemberNo_NotFoundAccount, //453 : 유저 연결정보 없음
        UserAccountByMemberNo_NotFoundUserAccount, //454 : 유저정보 없음

        SaveUserAccount_UserAccountRequired, //455 : 사용자 계정정보 필요
        SaveUserAccount_UIDRequired, //456 : 사용자 계정 고유번호 필요
        SaveUserAccount_NickRequired, //457 : 사용자 닉네임 필요
        SaveUserAccount_ProfileIconIDRequired, //458 : 사용자 프로필 아이콘 정보 필요
        SaveUserAccount_ProfileBGIDRequired, //459 : 사용자 프로필 배경 정보 필요
        SaveUserAccount_HeroIDRequired, //460 : 사용자 영웅 정보 필요
        SaveUserAccount_PetUniqueIDRequired, //461 : 사용자 펫 정보 필요
        SaveUserAccount_VehicleIDRequired, //462 : 사용자 탈것 정보 필요
        SaveUserAccount_UserLevelRequired, //463 : 사용자 레벨 정보 필요
        SaveUserAccount_UserExpRequired, //464 : 사용자 경험치 정보 필요
        SaveUserAccount_LastClearChapterIDRequired, //465 : 글리치 던전 클리어 정보 필요
        SaveUserAccount_NotFoundUserAccount, //466 : 사용자 정보 없음
        SaveUserAccount_FailedToUpdate, //467 : 사용자 정보 수정 실패

        UserBlockByNick_NickRequired, //468 : 닉네임 필요
        UserBlockByNick_NotFoundUserAccount, //469 : 사용자 정보 검색실패
        UserBlockByNick_NotFoundAccount, //470 : 사용자 계정 연결정보 검색실패
        UserBlockByNick_NotFoundMemberNo, //471 : 스토브 계정 정보 없음
        UserBlockByNick_NotFoundData, //472 : 테이터 없음

        UserBlockByMemberNo_MemberNoRequired, //473 : 스마게 계정 아이디 필요
        UserBlockByMemberNo_MemberNoMustUInt64, //474 : MemberNo는 정수
        UserBlockByMemberNo_NotFoundAccount, //475 : 사용자 계정 연결정보 검색실패
        UserBlockByMemberNo_NotFoundUserAccount, //476 : 사용자 정보 검색실패
        UserBlockByMemberNo_NotFoundMemberNo, //477 : 스토브 계정 정보 없음
        UserBlockByMemberNo_NotFoundData, //478 : 테이터 없음

        UserBlockByUID_UIDRequired, //479 : 계정 고유 아이디 필요
        UserBlockByUID_UIDMustInt64, //480 : UID는 정수
        UserBlockByUID_NotFoundUserAccount, //481 : 사용자 정보 검색실패
        UserBlockByUID_NotFoundAccount, //482 : 사용자 계정 연결정보 검색실패
        UserBlockByUID_NotFoundMemberNo, //483 : 스토브 계정 정보 없음
        UserBlockByUID_NotFoundData, //484 : 테이터 없음

        SaveUserBlock_EmptyUserBlock, //485 : 입력할 사용자 블럭 정보 없음
        SaveUserBlock_MemberNoRequired, //486 : 스마게 계정 아이디 필요
        SaveUserBlock_UIDRequired, //487 : 사용자 유니크 아이디 필요
        SaveUserBlock_BlockReasonIDRequired, //488 : 블럭 사유 필요
        SaveUserBlock_BlockReasonStrRequired, //489 : 블럭사유 내용 필요
        SaveUserBlock_NotMatchedBlockReasonID, //490 : 일치하는 블럭사유 아이디 없음
        SaveUserBlock_NotFoundUserBlockForUpdate, //491 : 수정할 사용자 블럭정보 없음
        SaveUserBlock_StartTimeRequired, //492 : 블럭 시작시간 정보필요
        SaveUserBlock_EndTimeRequired, //493 : 블럭 종료시간 정보필요
        SaveUserBlock_FailedToInsert, //494 : 사용자 블럭정보 입력 실패
        SaveUserBlock_FailedToUpdate, //495 : 사용자 블럭정보 수정 실패

        DeleteUserBlock_EmptyUserBlock, //496 : 삭제할 사용자 블럭 정보 없음
        DeleteUserBlock_MemberNoRequired, //497 : 스마게 계정 아이디 필요
        DeleteUserBlock_SeqIDRequired, //498 : 사용자 블럭 고유번호 필요
        DeleteUserBlock_NotFoundUserBlock, //499 : 사용자 블럭 정보 검색실패
        DeleteUserBlock_FailedToDelete, //500 : 사용자 블럭 정보 삭제실패

        UserDevice_UIDRequired, //501 : 계정 고유번호 필요
        UserDevice_NotFoundData, //502 : 데이터 없음

        WeaponCategory_UIDRequired, //503 : 계정 고유번호 필요
        WeaponCategory_NotFoundData, //504 : 데이터 없음

        SaveWeaponCategories_EmptyData, //505 : 저장할 데이터 없음
        SaveWeaponCategories_UIDRequired, //506 : 계정 고유번호 필요
        SaveWeaponCategories_WeaponCategoryIDRequired, //507 : 무기 카테고리 아이디 필요
        SaveWeaponCategories_ExpZeroOrMore, //508 : 카테고리 레벨은 0이상
        SaveWeaponCategories_NotFoundUserAccount, //509 : 대상 사용자 정보없음
        SaveWeaponCategories_FailedToInsert, //510 : 테이터 추가 실패
        SaveWeaponCategories_FailedToUpdate, //511 : 테이터 추가 실패

        DeleteWeaponCategories_EmptyData, //512 : 저장할 데이터 없음
        DeleteWeaponCategories_UIDRequired, //513 : 계정 고유번호 필요
        DeleteWeaponCategories_WeaponCategoryIDRequired, //514 : 무기 카테고리 아이디 필요
        DeleteWeaponCategories_NotFoundUserAccount, //515 : 대상 사용자 정보없음
        DeleteWeaponCategories_NotFoundAnyData, //516 : 데이터 없음
        DeleteWeaponCategories_NotFoundData, //517 : 삭제할 데이터 없음
        DeleteWeaponCategories_FailedToDelete, //518 : 테이터 삭제 실패

        GMAuthorize_NotFoundCode, //519 : 인증 코드없음
        GMAuthorize_NotFoundClientId, //520 : 클라이언트 아이디 없음
        GMAuthorize_ClientIdMustGuid, //521 : 클라이언트 아이디 형식 오류
        GMAuthorize_NotAllowedClientId, //522 : 허가된 클라이언트 아이디 아님
        GMAuthorize_NotFoundUser, //523 : 유저정보 없음
        GMAuthorize_AlreadySignIn, //524 : 이미 로그인됨
        GMAuthorize_AuthExpired, //525 : 로그인 정보 만료
        GMAuthorize_FailedToReIssueToken, //526 : 토큰 재발급 실패
        GMAuthorize_FailedSignIn, //527 : 로그인 실패
        GMAuthorize_NotFoundToken, //528 : 토큰 확인 실패

        ManagesUser_UserIDRequired, //529 : 관리자 계정 고유번호 필요
        ManagesUser_NotFoundUser, //530 : 관리자 계정 정보 없음

        ManagesAddUser_RequiredUserName, //531 : 관리자 이름 필요
        ManagesAddUser_RequiredUserEmail, //532 : 관리자 이메일 필요
        ManagesAddUser_MustUseCompanyMail, //533 : 회사 이메일 필요
        ManagesAddUser_CanNotInsertSuper, //534 : 슈퍼계정 이름으로는 추가할수 없음
        ManagesAddUser_FailedToInsert, //535 : 추가 실패

        ManagesDeleteUser_RequiredUserID, //536 : 관리자 계정 고유번호 필요
        ManagesDeleteUser_NotFoundUser, //537 : 관리자 계정 정보 없음
        ManagesDeleteUser_NotFoundUserEmail, //538 : 관리자 이메일 정보 없음
        ManagesDeleteUser_CanNotDelSuperUser, //539 : 최고 관리자 계정 삭제 불가
        ManagesDeleteUser_FailedToDeleteUser, //540 : 삭제 실패

        ManagesRestoreUser_RequiredUserID, //541 : 관리자 계정 고유번호 필요
        ManagesRestoreUser_NotFoundUser, //542 : 관리자 계정 정보 없음
        ManagesRestoreUser_FailedToRestoreUser, //543 : 복원 실패

        ManagesResetCountFailedSignin_RequiredUserID, //544 : 관리자 계정 고유번호 필요
        ManagesResetCountFailedSignin_NotFoundUser, //545 : 관리자 계정 정보 없음
        ManagesResetCountFailedSignin_FailedToReset, //546 : 초기화 실패

        ManagesResetLatestSignIn_RequiredUserID, //547 : 관리자 계정 고유번호 필요
        ManagesResetLatestSignIn_NotFoundUser, //548 : 관리자 계정 정보 없음
        ManagesResetLatestSignIn_FailedToReset, //549 : 초기화 실패

        ManagesSaveUserRole_RequiredUserID, //550 : 관리자 계정 고유번호 필요
        ManagesSaveUserRole_NotFoundUser, //551 : 관리자 계정 정보 없음
        ManagesSaveUserRole_NotFoundUserEmail, //552 : 관리자 이메일 정보 없음
        ManagesSaveUserRole_CanNotModifySuperUser, //553 : 최고 관리자 계정 권한 수정 불가
        ManagesSaveUserRole_CanNotAddSuperRole, //554 : 최고 관리자 권한 부여 불가

        VersionInfos_FailedToSearchVersionInfo, //555 : 버전정보 검색실패

        VersionInfo_VersionRequired, //556 : 버전 정보필요
        VersionInfo_PlatformRequired, //557 : 플랫폼 정보필요
        VersionInfo_NotFoundData, //558 : 버전정보 검색실패

        SaveVersionInfo_EmptyVersionInfo, //559 : 입력할 버전정보 없음
        SaveVersionInfo_VersionRequired, //560 : 버전 정보필요
        SaveVersionInfo_PlatformRequired, //561 : 플랫폼 정보필요
        SaveVersionInfo_IsValidRequired, //562 : 사용여부 정보필요
        SaveVersionInfo_ServerStateRequired, //563 : 서버상태 정보필요
        SaveVersionInfo_ClientStateRequired, //564 : 클라이언트 상태 정보필요
        SaveVersionInfo_CDNInfoRequired, //565 : CDN 주소 정보필요
        SaveVersionInfo_DuplicateVersionInfo, //566 : 같은 버전의 데이터 있음
        SaveVersionInfo_NotFoundVersionInfoForUpdate, //567 : 수정할 버전정보 검색실패
        SaveVersionInfo_FailedToInsert, //568 : 버전정보 생성실패
        SaveVersionInfo_FailedToUpdate, //569 : 버전정보 수정실패

        DeleteVersionInfo_EmptyVersionInfo, //570 : 삭제할 버전정보 없음
        DeleteVersionInfo_VersionRequired, //571 : 버전 정보필요
        DeleteVersionInfo_PlatformRequired, //572 : 플랫폼 정보필요
        DeleteVersionInfo_NotFoundVersionInfo, //573 : 버전정보 검색실패
        DeleteVersionInfo_FailedToDelete, //574 : 버전정보 삭제실패

        DownloadVersionInfo_VersionRequired, //575 : 버전 정보필요
        DownloadVersionInfo_PlatformRequired, //576 : 플랫폼 정보필요
        DownloadVersionInfo_NotFoundVersionInfo, //577 : 버전정보 검색실패
        DownloadVersionInfo_FailedToSerializeData, //578 : 버전정보 Byte 변환 실패

        Maintenances_NotFoundMaintenances, //579 : 서비스 상태 정보 검색실패

        SaveMaintenances_EmptyMaintenance, //580 : 입력할 서비스 상태 정보 없음
        SaveMaintenances_PlatformRequired, //581 : 플랫폼 정보필요
        SaveMaintenances_AreaRequired, //582 : 지역 정보필요
        SaveMaintenances_StateRequired, //583 : 점검상태 정보필요
        SaveMaintenances_StartTimeRequired, //584 : 점검 시작시간 정보필요
        SaveMaintenances_EndTimeRequired, //585 : 점검 종료시간 정보필요
        SaveMaintenances_NoticeStrIDRequired, //586 : 공지 문구 ID 필요
        SaveMaintenances_UpdateStrIDRequired, //587 : 업데이트 문구 ID 필요
        SaveMaintenances_RecomUpdateStrIDRequired, //588 : 권장 업데이트 문구 ID 필요
        SaveMaintenances_DuplicateMaintenance, //589 : 같은 플랫폼 지역의 데이터 있음
        SaveMaintenances_NotFoundMaintenanceForUpdate, //590 : 수정할 서비스 상태 정보 검색실패
        SaveMaintenances_FailedToInsert, //591 : 서비스 상태 정보 생성실패
        SaveMaintenances_FailedToUpdate, //592 : 서비스 상태 정보 수정실패

        DeleteMaintenance_EmptyMaintenance, //593 : 삭제할 서비스 상태 정보 없음
        DeleteMaintenance_MaintenanceIDRequired, //594 : 삭제할 서비스 상태 ID 필요
        DeleteMaintenance_NotFoundData, //595 : 서비스 상태 정보 검색실패
        DeleteMaintenance_FailedToDelete, //596 : 서비스 상태 정보 삭제실패

        ChattingNotice_ChattingNoticeIDRequired, //597 : 채팅공지 아이디 필요
        ChattingNotice_ChattingNoticeIDMustGuid, //598 : 채팅공지 아이디는 Guid 형식
        ChattingNotice_ChattingNoticeIDEmpty, //599 : 채팅공지 아이디 변환 실패
        ChattingNotice_NotFoundData, //600 : 채팅공지 정보 검색실패

        SaveChattingNotices_EmptyChattingNotice, //601 : 입력할 채팅공지 정보 없음
        SaveChattingNotices_IDRequired, //602 : 채팅공지 아이디 필요
        SaveChattingNotices_MessageRequired, //603 : 공지내용 필요
        SaveChattingNotices_NoticeTypeRequired, //604 : 공지형식 필요
        SaveChattingNotices_VisibleTimeRequired, //605 : 공지 유지시간 필요
        SaveChattingNotices_VisibleCountRequired, //606 : 공지 반복횟수 필요
        SaveChattingNotices_NoticeTimeRequired, //607 : 공지시간 필요
        SaveChattingNotices_NotFoundChattingNoticeForUpdate, //608 : 수정할 채팅공지 정보 검색실패
        SaveChattingNotices_FailedToInsert, //609 : 채팅공지 추가실패
        SaveChattingNotices_FailedToUpdate, //610 : 채팅공지 수정실패

        DeleteChattingNotice_EmptyChattingNotice, //611 : 삭제할 채팅공지 정보 없음
        DeleteChattingNotice_ChattingNoticeIDRequired, //612 : 삭제할 채팅공지 ID 필요
        DeleteChattingNotice_NotFoundChattingNotice, //613 : 채팅공지 정보 검색실패
        DeleteChattingNotice_FailedToDelete, //614 : 채팅공지 정보 삭제실패

        ImmediatelyChattingNotice_EmptyChattingNotice, //615 : 즉시 공지할 채팅공지 정보 없음
        ImmediatelyChattingNotice_ChattingNoticeIDRequired, //616 : 즉시 공지할 채팅공지 ID 필요
        ImmediatelyChattingNotice_NotFoundChattingNotice, //617 : 즉시 공지할 채팅공지 정보 검색실패
        ImmediatelyChattingNotice_FailedToNotice, //618 : 채팅 즉시공지 실패

        SearchUserAccounts_UIDRequired, //619 : 검색할 사용자 UID 필요
        SearchUserAccounts_UIDMustInt64, //620 : UID는 정수
        SearchUserAccounts_NotFoundUserAccount, //621 : 사용자 정보 없음

        SearchUserAccountsByMemberNo_MemberNoRequired, //622 : 검색할 사용자 UID 필요
        SearchUserAccountsByMemberNo_MemberNoMustUInt64, //623 : UID는 정수
        SearchUserAccountsByMemberNo_NotFoundUserAccount, //624 : 사용자 정보 없음

        ImmediatelySendMail_UIDRequired, //625 : 우편 발송대상 사용자 UID 필요
        ImmediatelySendMail_UIDMustInt64, //626 : UID는 정수
        ImmediatelySendMail_NotFoundUserAccount, //627 : 사용자 정보 없음
        ImmediatelySendMail_EmptyToSendMail, //628 : 즉시 발송할 우편 정보 없음
        ImmediatelySendMail_TitleRequired, //629 : 우편제목 필요
        ImmediatelySendMail_ItemIDRequired, //630 : 발송할 아이템 아이디 필요
        ImmediatelySendMail_ItemCountMustUpperZero, //631 : 발송할 아이템 수량은 0이상
        ImmediatelySendMail_MessageRequired, //632 : 우편내용 필요
        ImmediatelySendMail_FailedToClaimMailID, //633 : 우편 아이디 발급실패
        ImmediatelySendMail_FailedToSendMail, //634 : 우편발송 실패

        SendGameMail_UIDRequired, //635 : 우편 발송대상 사용자 UID 필요
        SendGameMail_UIDMustInt64, //636 : UID는 정수
        SendGameMail_NotFoundUserAccount, //637 : 사용자 정보 없음
        SendGameMail_EmptyToSendMail, //638 : 즉시 발송할 우편 정보 없음
        SendGameMail_TitleRequired, //639 : 우편제목 필요
        SendGameMail_ItemIDRequired, //640 : 발송할 아이템 아이디 필요
        SendGameMail_ItemCountMustUpperZero, //641 : 발송할 아이템 수량은 0이상
        SendGameMail_MessageRequired, //642 : 우편내용 필요
        SendGameMail_FailedToClaimMailID, //643 : 우편 아이디 발급실패
        SendGameMail_FailedToSendMail, //644 : 우편발송 실패
        SendGameMail_NotSuitableUserId, //645 : 부적합한 관리자 아이디
        SendGameMail_FailedToInsertJob, //646 : 일괄처리 작업 등록실패

        ManagerLogs_StartTimeRequired, //647 : 시작일시 필요
        ManagerLogs_StartTimeNotSuitableDateTime, //648 : 시작일시 날짜형식 안맞음
        ManagerLogs_EndTimeRequired, //649 : 종료일시 필요
        ManagerLogs_EndTimeNotSuitableDateTime, //650 : 시작일시 날짜형식 안맞음
        ManagerLogs_SearchPeriodLimit, //651 : 검색기간 넘음
        ManagerLogs_FailedToSearch, //652 : 검색 실패
        ManagerLogs_NotFoundData, //653 : 검색된 데이터 없음

        DownloadManagerLogs_StartTimeRequired, //654 : 시작일시 필요
        DownloadManagerLogs_StartTimeNotSuitableDateTime, //655 : 시작일시 날짜형식 안맞음
        DownloadManagerLogs_EndTimeRequired, //656 : 종료일시 필요
        DownloadManagerLogs_EndTimeNotSuitableDateTime, //657 : 시작일시 날짜형식 안맞음
        DownloadManagerLogs_SearchPeriodLimit, //658 : 검색기간 넘음
        DownloadManagerLogs_FailedToSearch, //659 : 검색 실패
        DownloadManagerLogs_NotFoundData, //660 : 검색된 데이터 없음
        DownloadManagerLogs_FailedToConvertExcel, //661 : 엑셀 데이터 변환 실패
        DownloadManagerLogs_FailedToSerializeData, //662 : Byte 변환 실패

        BiskitLogs_StartTimeRequired, //663 : 시작일시 필요
        BiskitLogs_StartTimeNotSuitableDateTime, //664 : 시작일시 날짜형식 안맞음
        BiskitLogs_EndTimeRequired, //665 : 종료일시 필요
        BiskitLogs_EndTimeNotSuitableDateTime, //666 : 시작일시 날짜형식 안맞음
        BiskitLogs_UserSearchTypeRequired, //667 : 사용자 검색방식 구분값 필요
        BiskitLogs_OneOrMoreSearchValueRequired, //668 : 하나 이상의 검색값 필요
        BiskitLogs_NotSuitableEventID, //669 : 적합한 이벤트 아이디 없음
        BiskitLogs_FailedToSearch, //670 : 검색 실패
        BiskitLogs_TooManyData, //671 : 검색된 데이터가 너무 많음
        BiskitLogs_NotFoundData, //672 : 검색된 데이터 없음

        DownloadGameLogs_StartTimeRequired, //673 : 시작일시 필요
        DownloadGameLogs_StartTimeNotSuitableDateTime, //674 : 시작일시 날짜형식 안맞음
        DownloadGameLogs_EndTimeRequired, //675 : 종료일시 필요
        DownloadGameLogs_EndTimeNotSuitableDateTime, //676 : 시작일시 날짜형식 안맞음
        DownloadGameLogs_UserSearchTypeRequired, //677 : 사용자 검색방식 구분값 필요
        DownloadGameLogs_OneOrMoreSearchValueRequired, //678 : 하나 이상의 검색값 필요
        DownloadGameLogs_NotSuitableEventID, //679 : 적합한 이벤트 아이디 없음
        DownloadGameLogs_EventIdSeqIDMustInteger, //680 : 이벤트 아이디 고유 아이디는 숫자
        DownloadGameLogs_NotFoundSuitableEventId, //681 : 적합한 이벤트 아이디 정보 없음
        DownloadGameLogs_FailedToSearch, //682 : 검색 실패
        DownloadGameLogs_TooManyData, //683 : 검색된 데이터가 많음
        DownloadGameLogs_NotFoundData, //684 : 검색된 데이터 너무 없음
        DownloadGameLogs_FailedToConvertExcel, //685 : 엑셀 데이터 변환 실패
        DownloadGameLogs_FailedToSerializeData, //686 : Byte 변환 실패

        NoticeBanner_NotFoundData, //687 : 검색실패

        SaveNoticeBanners_EmptySaveData, //688 : 저장할 데이터 없음
        SaveNoticeBanners_TitleRequired, //689 : 팝업배너 제목 필요
        SaveNoticeBanners_FailedToInsert, //690 : 팝업배너 추가실패
        SaveNoticeBanners_FailedToUpdate, //691 : 팝업배너 수정실패

        DeleteNoticeBanners_EmptySaveData, //692 : 저장할 데이터 없음
        DeleteNoticeBanners_BannerIDRequired, //693 : 팝업배너 아이디 필요
        DeleteNoticeBanners_NotFoundData, //694 : 팝업배너 없음
        DeleteNoticeBanners_FailedToDelete, //695 : 팝업배너 삭제실패

        InitEmailPassword_ConfirmIDRequired, //696 : 컨펌 아이디 필요
        InitEmailPassword_PasswordRequired, //697 : 비밀번호 필요
        InitEmailPassword_NotEnoughPasswordRule, //698 : 비밀번호 규칙 안맞음
        InitEmailPassword_NotFoundData, //699 : 계정 없음
        InitEmailPassword_AlreadyUsedPassword, //700 : 이미 사용된 비밀번호
        InitEmailPassword_FailedToInit, //701 : 비밀번호 초기화 실패
        InitEmailPassword_FailedToConfirm, //702 : 비밀번호 초기화 실패

        SignInEmail_ClientIdRequired, //703 : 클라이언트 아이디 필요
        SignInEmail_ClientIdMustGuid, //704 : 클라이언트 아이디는 Guid 형식
        SignInEmail_NotAllowedClientId, //705 : 허가된 클라이언트 아이디 아님
        SignInEmail_AlreadySignIn, //706 : 이미 로그인됨
        SignInEmail_EmailRequired, //707 : 이메일 입력 필요
        SignInEmail_MustUseCompanyMail, //708 : 회사 메일이 아님
        SignInEmail_PasswordRequired, //709 : 비밀번호 입력 필요
        SignInEmail_ConfirmRequired, //710 : 비밀번호 초기화 중인 계정
        SignInEmail_DoNotMatchedData, //711 : 이메일 또는 비밀번호 불일치
        SignInEmail_TooManyFailedSignin, //712 : 로그인 실패 허용횟수 초과
        SignInEmail_TooLongLatestSignin, //713 : 마지막 로그인 오래 경과됨
        SignInEmail_OAuthEmailNull, //714 : 인증된 이메일 정보 없음
        SignInEmail_OAuthNameNull, //715 : 인증된 이름 정보 없음
        SignInEmail_OAuthUidNull, //716 : 인증된 고유번호 정보 없음
        SignInEmail_FailedToSaveToken, //717 : 토큰 저장실패
        SignInEmail_FailedSignIn, //718 : 인증 실패
        SignInEmail_CanNotTrySignIn, //719 : 인증시도 실패

        ChangePassword_PasswordRequired, //720 : 비밀번호 필요
        ChangePassword_NewPasswordRequired, //721 : 새 비밀번호 필요
        ChangePassword_NewPasswordConfirmRequired, //722 : 새 비밀번호 확인 필요
        ChangePassword_NewMustDifferentPassword, //723 : 새 비밀번호는 기존 비밀번호와 다르게
        ChangePassword_NotMatchPasswordConfirm, //724 : 새 비밀번호 와 비밀번호 확인 불일치
        ChangePassword_NotEnoughPasswordRule, //725 : 비밀번호 규칙 안맞음
        ChangePassword_NotEnoughNewPasswordRule, //726 : 새 비밀번호 규칙 안맞음
        ChangePassword_NotEnoughNewPasswordConfirmRule, //727 : 새 비밀번호 확인 규칙 안맞음
        ChangePassword_NotFoundUserData, //728 : 유저정보 없음
        ChangePassword_NotMatchPassword, //729 : 기존 비밀번호 불일치
        ChangePassword_AlreadyUsedPassword, //730 : 이미 사용된 패스워드
        ChangePassword_FailedToChange, //731 : 비밀번호 변경 실패

        SetRefreshPassword_EmailRequired, //732 : 비밀번호 필요
        SetRefreshPassword_NotFoundUserData, //733 : 유저정보 없음
        SetRefreshPassword_AlreadyInit, //734 : 이미 초기화 상태
        SetRefreshPassword_FailedToInit, //735 : 비밀번호 초기화 실패

        SaveOpenWorldTime_MinuteMust0To1440, //736 : 서버시간(분)은 0과 1440사이
        SaveOpenWorldTime_MultiplyMust0Point1To10, //737 : 시작 분(Minute)은 0.1과 10사이
        SaveOpenWorldTime_FailedToSaveData, //738 : 오픈월드 시간 저장 실패
        SaveOpenWorldTime_FailedToPub, //739 : 오픈월드 시간변경 서버로 Pub 실패

        SaveGoldClashTime_StartHourMust0To23, //740 : 시작 시(Hour)는 0과 23사이
        SaveGoldClashTime_StartMinuteMust0To59, //741 : 시작 분(Minute)은 0과 59사이
        SaveGoldClashTime_EndHourMust0To23, //742 : 종료 시(Hour)는 0과 23사이
        SaveGoldClashTime_EndMinuteMust0To59, //743 : 종료 분(Minute)은 0과 59사이
        SaveGoldClashTime_FailedToSaveData, //744 : 골드클래시 오픈 시간 저장 실패

        SilverMedalStores_UIDRequired, //745 : 계정 고유번호 필요
        SilverMedalStores_NotFoundData, //746 : 데이터 없음

        EventStores_UIDRequired, //747 : 계정 고유번호 필요
        EventStores_NotFoundData, //748 : 데이터 없음

        Expressions_UIDRequired, //749 : 계정 고유번호 필요
        Expressions_NotFoundData, //750 : 데이터 없음

        ExpressionPresets_UIDRequired, //751 : 계정 고유번호 필요
        ExpressionPresets_NotFoundData, //752 : 데이터 없음

        WonderCubes_UIDRequired, //753 : 계정 고유번호 필요
        WonderCubes_NotFoundData, //754 : 데이터 없음

        SaveWonderCubes_EmptyData, //755 : 저장할 데이터 없음
        SaveWonderCubes_UIDRequired, //756 : 계정 고유번호 필요
        SaveWonderCubes_SlotIDRequired, //757 : 원더큐브 슬롯 아이디 필요
        SaveWonderCubes_WonderCubeIDRequired, //758 : 원더큐브 아이디 필요
        SaveWonderCubes_NotFoundUserAccount, //759 : 대상 사용자 정보없음
        SaveWonderCubes_NotFoundData, //760 : 수정할 테이터 없음
        SaveWonderCubes_FailedToUpdate, //761 : 테이터 수정 실패

        WonderStores_UIDRequired, //762 : 계정 고유번호 필요
        WonderStores_NotFoundData, //763 : 데이터 없음

        SaveWonderStores_EmptyData, //764 : 저장할 데이터 없음
        SaveWonderStores_UIDRequired, //765 : 계정 고유번호 필요
        SaveWonderStores_StoreIDRequired, //766 : 상점 아이디 필요
        SaveWonderStores_NotFoundUserAccount, //767 : 대상 사용자 정보없음
        SaveWonderStores_NotFoundData, //768 : 수정할 테이터 없음
        SaveWonderStores_FailedToUpdate, //769 : 테이터 수정 실패

        LobbyServerTimes_FailedToUpdateLobbyServerTime, //770 : 로비서버 시간 갱신 실패
        LobbyServerTimes_LobbyServerTimeEmpty, //771 : 로비서버 시간정보 없음

        SaveLobbyServerTimes_LobbyServerTimeRequired, //772 : 변경할 로비서버 시간정보 필요
        SaveLobbyServerTimes_FailedToSaveLobbyServerTime, //773 : 로비서버 시간 저장실패
        SaveLobbyServerTimes_LobbyServerTimeEmpty, //774 : 로비서버 시간정보 없음

        BlockContents_NotFoundBlockContents, //775 : 검색된 블럭 컨텐츠 정보 없음

        SaveBlockContents_BlockContentsEmpty, //776 : 블럭할 컨텐츠 없음
        SaveBlockContents_PacketIdEmpty, //777 : 블럭할 컨텐츠의 패킷 아이디 없음
        SaveBlockContents_NotSuitablePacketId, //778 : 패킷 아이디에 해당하는 블럭 컨텐츠 없음
        SaveBlockContents_AlreadyBlocked, //779 : 이미 블럭된 컨텐츠
        SaveBlockContents_FailedToInsert, //780 : 블럭 컨텐츠 저장 실패

        DeleteBlockContents_BlockContentsEmpty, //781 : 삭제할 블럭 컨텐츠 없음
        DeleteBlockContents_PacketIdEmpty, //782 : 패킷 아이디 없음
        DeleteBlockContents_NotFoundBlockContents, //783 : 삭제할 블럭 컨텐츠 없음
        DeleteBlockContents_FailedToDelete, //784 : 블럭 컨텐츠 삭제 실패

        SelectEventMail_ParametersEmpty, //785 : 검색할 데이터 없음
        SelectEventMail_IDRequired, //786 : 이벤트 메일 아이디 필요
        SelectEventMail_NotFoundData, //787 : 수정할 이벤트 메일 없음

        InsertEventMail_ParametersEmpty, //788 : 저장할 데이터 없음
        InsertEventMail_TitleRequired, //789 : 이벤트 메일 제목 필요
        InsertEventMail_MessageRequired, //790 : 이벤트 메일 내용 필요
        InsertEventMail_RewardTypeRequired, //791 : 보상 아이템 타입 필요
        InsertEventMail_RewardIDRequired, //792 : 보상 아이템 아이디 필요
        InsertEventMail_RewardCountRequired, //793 : 보상 아이템 수량은 0 이상
        InsertEventMail_EndTimeLaterThanStartTime, //794 : 이벤트 메일 종료시간은 시작시간보다 이후여야 함
        InsertEventMail_NotFoundData, //795 : 수정할 이벤트 메일 없음
        InsertEventMail_FailedToInsert, //796 : 이벤트 우편 입력실패
        InsertEventMail_NotSuitableUserId, //797 : 부적합한 관리자 아이디

        UpdateEventMail_ParametersEmpty, //798 : 저장할 데이터 없음
        UpdateEventMail_MailIDRequired, //799 : 이벤트 메일 아이디 필요
        UpdateEventMail_TitleRequired, //800 : 이벤트 메일 제목 필요
        UpdateEventMail_MessageRequired, //801 : 이벤트 메일 내용 필요
        UpdateEventMail_RewardTypeRequired, //802 : 보상 아이템 타입 필요
        UpdateEventMail_RewardIDRequired, //803 : 보상 아이템 아이디 필요
        UpdateEventMail_RewardCountRequired, //804 : 보상 아이템 수량은 0 이상
        UpdateEventMail_EndTimeLaterThanStartTime, //805 : 이벤트 메일 종료시간은 시작시간보다 이후여야 함
        UpdateEventMail_NotFoundData, //806 : 수정할 이벤트 메일 없음
        UpdateEventMail_FailedToUpdate, //807 : 이벤트 우편 수정실패
        UpdateEventMail_NotSuitableUserId, //808 : 부적합한 관리자 아이디

        RemoveEventMail_ParametersEmpty, //809 : 삭제할 데이터 없음
        RemoveEventMail_MailIDRequired, //810 : 이벤트 메일 ID필요
        RemoveEventMail_NotFoundData, //811 : 삭제할 데이터 없음
        RemoveEventMail_FailedToRemove, //812 : 데이터 삭제 실패
        RemoveEventMail_NotSuitableUserId, //813 : 부적합한 관리자 아이디

        GameRanking_UnknownGameType, //814 : 알수없는 게임
        GameRanking_DateTimeRequired, //815 : 날짜 필요

        DeleteMails_ParameterEmpty, //816 : 삭제할 데이터 정보 없음
        DeleteMails_IDRequired, //817 : 메일 ID필요
        DeleteMails_NotFoundData, //818 : 삭제할 데이터 없음
        DeleteMails_AlreadyCheckedMail, //819 : 이미 사용자가 확인한 우편
        DeleteMails_ActiveUser, //820 : 사용자가 로그인 중
        DeleteMails_FailedToDelete, //821 : 데이터 삭제 실패

        SaveWhiteList_ParameterEmpty, //822 : 저장할 데이터 없음
        SaveWhiteList_DeviceIDORMemberNoRequired, //823 : 디바이스 아이디 또는 StoveMemberNo 필요
        SaveWhiteList_CommentRequired, //824 : 코멘트 필요
        SaveWhiteList_DuplicateDeviceIDAndMemberNo, //825 : 데이터 중복
        SaveWhiteList_AlreadyExistsData, //826 : 데이터 중복
        SaveWhiteList_FailedToSaveSomeData, //827 : 일부 데이터 저장 실패
        SaveWhiteList_FailedToSaveData, //828 : 데이터 저장 실패

        DeleteWhiteList_ParameterEmpty, //829 : 저장할 데이터 없음
        DeleteWhiteList_IDRequired, //830 : 삭제할 데이터의 ID 필요
        DeleteWhiteList_NotFoundData, //831 : 삭제할 데이터 없음
        DeleteWhiteList_FailedToDeleteSomeData, //832 : 일부 데이터 삭제 실패
        DeleteWhiteList_FailedToDeleteData, //833 : 데이터 삭제 실패

        SaveBlockIP_ParameterEmpty, //834 : 저장할 데이터 없음
        SaveBlockIP_IPAddressRequired, //835 : IP주소 필요
        SaveBlockIP_ReasonRequired, //836 : 블럭사유 필요
        SaveBlockIP_AlreadyExistsData, //837 : 데이터 중복
        SaveBlockIP_FailedToSaveSomeData, //838 : 일부 데이터 저장 실패
        SaveBlockIP_FailedToSaveData, //839 : 데이터 저장 실패

        DeleteBlockIP_ParameterEmpty, //840 : 저장할 데이터 없음
        DeleteBlockIP_IDRequired, //841 : 삭제할 데이터의 ID 필요
        DeleteBlockIP_NotFoundData, //842 : 삭제할 데이터 없음
        DeleteBlockIP_FailedToDeleteSomeData, //843 : 일부 데이터 삭제 실패
        DeleteBlockIP_FailedToDeleteData, //844 : 데이터 삭제 실패

        SaveSlang_ParameterEmpty, //845 : 저장할 데이터 없음
        SaveSlang_WordRequired, //846 : IP주소 필요
        SaveSlang_AlreadyExistsData, //847 : 데이터 중복
        SaveSlang_FailedToSaveSomeData, //848 : 일부 데이터 저장 실패
        SaveSlang_FailedToSaveData, //849 : 데이터 저장 실패

        DeleteSlang_ParameterEmpty, //850 : 저장할 데이터 없음
        DeleteSlang_IDRequired, //851 : 삭제할 데이터의 ID 필요
        DeleteSlang_NotFoundData, //852 : 삭제할 데이터 없음
        DeleteSlang_FailedToDeleteSomeData, //853 : 일부 데이터 삭제 실패
        DeleteSlang_FailedToDeleteData, //854 : 데이터 삭제 실패

        SaveFirewall_ParameterEmpty, //855 : 저장할 데이터 없음
        SaveFirewall_IPAddressRequired, //856 : IP주소 필요
        SaveFirewall_DescriptionRequired, //857 : IP주소 필요
        SaveFirewall_AlreadyExistsData, //858 : 데이터 중복
        SaveFirewall_FailedToSaveSomeData, //859 : 일부 데이터 저장 실패
        SaveFirewall_FailedToSaveData, //860 : 데이터 저장 실패

        DeleteFirewall_ParameterEmpty, //861 : 저장할 데이터 없음
        DeleteFirewall_IDRequired, //862 : 삭제할 데이터의 ID 필요
        DeleteFirewall_IDMustGuid, //863 : 삭제할 데이터의 ID는 Guid 형식
        DeleteFirewall_NotFoundData, //864 : 삭제할 데이터 없음
        DeleteFirewall_FailedToDeleteSomeData, //865 : 일부 데이터 삭제 실패
        DeleteFirewall_FailedToDeleteData, //866 : 데이터 삭제 실패

        ChatLogs_StartTimeRequired, //867 : 시작일시 필요
        ChatLogs_StartTimeNotSuitableDateTime, //868 : 시작일시 날짜형식 안맞음
        ChatLogs_EndTimeRequired, //869 : 종료일시 필요
        ChatLogs_EndTimeNotSuitableDateTime, //870 : 시작일시 날짜형식 안맞음
        ChatLogs_SearchPeriodLimit, //871 : 검색기간 넘음

        ManagerServerLogs_StartTimeRequired, //872 : 시작일시 필요
        ManagerServerLogs_StartTimeNotSuitableDateTime, //873 : 시작일시 날짜형식 안맞음
        ManagerServerLogs_EndTimeRequired, //874 : 종료일시 필요
        ManagerServerLogs_EndTimeNotSuitableDateTime, //875 : 시작일시 날짜형식 안맞음
        ManagerServerLogs_SearchPeriodLimit, //876 : 검색기간 넘음

        GMUserService_ConnectionIdRequired, //877 : 연결 아이디 필요
        GMUserService_ConnectionIdAndJobIdRequired, //878 : 연결 아이디, 작업 아이디 필요
        GMUserService_NotFoundUser, //879 : 관리자 정보 없음
        GMUserService_NotFoundUserJob, //880 : 관리자 작업정보 없음
        GMUserService_OnProcessingJob, //881 : 처리중인 작업
        GMUserService_CompletedJob, //882 : 완료된 작업
        GMUserService_FailedToUpdateConnectedUser, //883 : 연결된 관리자 정보 수정 실패
        GMUserService_FailedToRemoveConnectedUser, //884 : 연결된 관리자 정보 제거 실패
        GMUserService_FailedToGetConnectedUsers, //885 : 연결된 관리자 확인 실패
        GMUserService_FailedToGetUserJobs, //886 : 관리자 작업 정보 확인 실패
        GMUserService_UserJobTypeRequired, //887 : 관리자 작업 형식 필요
        GMUserService_UserJobCountRequired, //888 : 관리자 작업 형식 필요
        GMUserService_FailedToAddUserJobCount, //889 : 관리자 작업횟수 기록 실패
        GMUserService_FailedToClearUserJob, //890 : 관리자 작업 완료처리 실패

        MailWithUsers_UIDRequired, //891 : 계정 고유번호 필요
        MailWithUsers_StartTimeRequired, //892 : 시작일시 필요
        MailWithUsers_StartTimeNotSuitableDateTime, //893 : 시작일시 날짜형식 안맞음
        MailWithUsers_EndTimeRequired, //894 : 종료일시 필요
        MailWithUsers_EndTimeNotSuitableDateTime, //895 : 시작일시 날짜형식 안맞음
        MailWithUsers_SearchPeriodLimit, //896 : 검색기간 넘음
        MailWithUsers_NotFoundData, //897 : 우편정보 없음

        DeleteMailWithUsers_MailIDRequired, //898 : 우편 고유번호 필요
        DeleteMailWithUsers_AlreadyReceivedMail, //899 : 이미 수령한 메일
        DeleteMailWithUsers_NotFoundData, //900 : 우편정보 없음
        DeleteMailWithUsers_FailedToDeleteSomeData, //901 : 일부 데이터 삭제 실패

        UserBillings_UIDRequired, //902 : 계정 고유번호 필요
        UserBillings_NotFoundData, //903 : 데이터 없음

        Billings_UIDRequired, //904 : 계정 고유번호 필요
        Billings_StartTimeRequired, //905 : 시작일시 필요
        Billings_StartTimeNotSuitableDateTime, //906 : 시작일시 날짜형식 안맞음
        Billings_EndTimeRequired, //907 : 종료일시 필요
        Billings_EndTimeNotSuitableDateTime, //908 : 시작일시 날짜형식 안맞음
        Billings_SearchPeriodLimit, //909 : 검색기간 넘음
        Billings_NotFoundData, //910 : 우편정보 없음

        Incubations_UIDRequired, //911 : 계정 고유번호 필요
        Incubations_NotFoundData, //912 : 데이터 없음

        InstantGuides_UIDRequired, //913 : 계정 고유번호 필요
        InstantGuides_NotFoundData, //914 : 데이터 없음

        ExpireLogSetting_NotFoundData, //915 : 데이터 없음

        SaveExpireLogSetting_LessThanMinExpireDay, //916 : 최소 만료 기간은 30일
        SaveExpireLogSetting_MoreThanMaxExpireDay, //917 : 최소 만료 기간은 1095일
        SaveExpireLogSetting_FailedToSaveData, //918 : 저장실패

        SendChattingMessage_ChattingMessageRequired, //919 : 채팅 메시지 필요
        SendChattingMessage_IdRequired, //920 : 채팅 메시지 아이디 필요
        SendChattingMessage_MessageRequired, //921 : 채팅 내용 필요
        SendChattingMessage_SendSigninIdRequired, //922 : 발신자 인증 아이디 필요
        SendChattingMessage_SendUserNameRequired, //923 : 발신자 이름 필요
        SendChattingMessage_NotFoundUserInfo, //924 : 대상 사용자 없음
        SendChattingMessage_NotFoundUserConnection, //925 : 대상 사용자 연결정보 없음
    }
