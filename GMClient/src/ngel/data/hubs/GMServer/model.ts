import { Models } from '@ngeldata/models/index';
import { DataTableModels } from '@ngeldata/tables/model';
import { Defines } from '@ngeldata/autoDefines';
import { Errors } from '@ngeldata/autoErrors';
import { v4 as uuidv4, NIL } from 'uuid';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';

export namespace GMServerHubModels {

    export class PConnectedAck {
        constructor(data?: PConnectedAck) {
            if (data) {
                this.error = data.error;
                this.serverId = data.serverId;
                this.serverVersion = data.serverVersion;
                this.recommendClientMasterVersion = data.recommendClientMasterVersion;
                this.recommendClientUpdateVersion = data.recommendClientUpdateVersion;
                this.recommendClientMaintenanceVersion = data.recommendClientMaintenanceVersion;
            }
        }

        error: Errors = Errors.None;
        serverId: string = '';
        serverVersion: string = '';
        recommendClientMasterVersion: number = 0;
        recommendClientUpdateVersion: number = 0;
        recommendClientMaintenanceVersion: number = 0;
    }

    export class PCheckAuthenticationAck {
        constructor(data?: PCheckAuthenticationAck) {
            if (data) {
                this.error = data.error;
                this.token = data.token;
                this.user = data.user;
                this.dataTable = data.dataTable;
            }
        }

        error: Errors = Errors.None;
        token?: string|null = '';
        user?: Models.SignInUser|null = null;
        dataTable?: DataTableModels.DataTable|null = null;
    }

    export class PCheckConnectionAck {
        constructor(data?: PCheckConnectionAck) {
            if (data) {
                this.error = data.error;
                this.token = data.token;
            }
        }

        error: Errors = Errors.None;
        token?: string|null = '';
    }

    export class PCommonNoticeAck {
        constructor(data?: PCommonNoticeAck) {
            if (data) {
                this.error = data.error;
                this.message = data.message;
            }
        }

        error: Errors = Errors.None;
        message?: string|null = '';
    }

    export class PSignInAck {
        constructor(data?: PSignInAck) {
            if (data) {
                this.error = data.error;
                this.user = data.user;
                this.token = data.token;
            }
        }

        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        token: string = '';
    }

    export class PSignInLDAPAck {
        constructor(data?: PSignInLDAPAck) {
            if (data) {
                this.error = data.error;
                this.user = data.user;
                this.token = data.token;
            }
        }

        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        token: string = '';
    }

    export class PSignInEmailAck {
        constructor(data?: PSignInEmailAck) {
            if (data) {
                this.error = data.error;
                this.user = data.user;
                this.countFailedSignin = data.countFailedSignin;
                this.token = data.token;
            }
        }

        error: Errors = Errors.None;
        user?: Models.SignInUser|null = null;
        countFailedSignin: number = 0;
        token: string = '';
    }

    export class PDataTableAck {
        constructor(data?: PDataTableAck) {
            if (data) {
                this.error = data.error;
                this.dataTable = data.dataTable;
            }
        }

        error: Errors = Errors.None;
        dataTable?: DataTableModels.DataTable|null = null;
    }

    export class PSendGameMailResultAck {
        constructor(data?: PSendGameMailResultAck) {
            if (data) {
                this.error = data.error;
                this.ackNoticeType = data.ackNoticeType;
                this.errorIndex = data.errorIndex;
                this.message = data.message;
            }
        }

        error: Errors = Errors.None;
        ackNoticeType: Defines.AckNoticeType = 0;
        errorIndex: number = 0;
        message: string = '';
    }

    export class PInsertEventMailResultAck {
        constructor(data?: PInsertEventMailResultAck) {
            if (data) {
                this.error = data.error;
                this.ackNoticeType = data.ackNoticeType;
                this.errorIndex = data.errorIndex;
                this.message = data.message;
            }
        }

        error: Errors = Errors.None;
        ackNoticeType: Defines.AckNoticeType = 0;
        errorIndex: number = 0;
        message: string = '';
    }

    export class PUpdateEventMailResultAck {
        constructor(data?: PUpdateEventMailResultAck) {
            if (data) {
                this.error = data.error;
                this.ackNoticeType = data.ackNoticeType;
                this.errorIndex = data.errorIndex;
                this.message = data.message;
            }
        }

        error: Errors = Errors.None;
        ackNoticeType: Defines.AckNoticeType = 0;
        errorIndex: number = 0;
        message: string = '';
    }

    export class PRemoveEventMailResultAck {
        constructor(data?: PRemoveEventMailResultAck) {
            if (data) {
                this.error = data.error;
                this.ackNoticeType = data.ackNoticeType;
                this.errorIndex = data.errorIndex;
                this.message = data.message;
            }
        }

        error: Errors = Errors.None;
        ackNoticeType: Defines.AckNoticeType = 0;
        errorIndex: number = 0;
        message: string = '';
    }

    export class PSendUserJobAck {
        constructor(data?: PSendUserJobAck) {
            if (data) {
                this.error = data.error;
                this.userJob = data.userJob;
            }
        }

        error: Errors = Errors.None;
        userJob: Models.UserJob|null = null;
    }

    export class PSignOutAck {
        constructor(data?: PSignOutAck) {
            if (data) {
                this.error = data.error;
            }
        }

        error: Errors = Errors.None;
    }

    export class PDeniedAck {
        constructor(data?: PDeniedAck) {
            if (data) {
                this.error = data.error;
            }
        }

        error: Errors = Errors.None;
    }

    export class PSendChattingMessageResultAck {
        constructor(data?: PSendChattingMessageResultAck) {
            if (data) {
                this.error = data.error;
                this.messageId = data.messageId;
            }
        }

        error: Errors = Errors.None;
        messageId: string = NIL;
    }

    export class PReceiveChattingMessageAck {
        constructor(data?: PReceiveChattingMessageAck) {
            if (data) {
                this.error = data.error;
                this.chattingMessage = data.chattingMessage;
            }
        }

        error: Errors = Errors.None;
        chattingMessage?: Models.ChattingMessage|null = null;
    }

    export class PTestAck {
        constructor(data?: PTestAck) {
            if (data) {
                this.error = data.error;
                this.isSigned = data.isSigned;
            }
        }

        error: Errors = Errors.None;
        isSigned: boolean = false;
    }

    export class PNoticeAck {
        constructor(data?: PNoticeAck) {
            if (data) {
                this.error = data.error;
            }
        }

        error: Errors = Errors.None;
    }

    export class PReceiveChattingMessageAllAck {
        constructor(data?: PReceiveChattingMessageAllAck) {
            if (data) {
                this.error = data.error;
                this.chattingMessage = data.chattingMessage;
            }
        }

        error: Errors = Errors.None;
        chattingMessage?: Models.ChattingMessage|null = null;
    }

    export class PCheckAuthenticationReq {
        constructor(data?: PCheckAuthenticationReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
    }

    export class PCheckConnectionReq {
        constructor(data?: PCheckConnectionReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
    }

    export class PSignInReq {
        constructor(data?: PSignInReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.clientId = data.clientId;
                this.code = data.code;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        clientId: string = '';
        code: string = '';
    }

    export class PSignInLDAPReq {
        constructor(data?: PSignInLDAPReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.clientId = data.clientId;
                this.email = data.email;
                this.password = data.password;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        clientId: string = '';
        email: string = '';
        password: string = '';
    }

    export class PSignInEmailReq {
        constructor(data?: PSignInEmailReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.clientId = data.clientId;
                this.email = data.email;
                this.password = data.password;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        clientId: string = '';
        email: string = '';
        password: string = '';
    }

    export class PSignOutReq {
        constructor(data?: PSignOutReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
    }

    export class PSendGameMailReq {
        constructor(data?: PSendGameMailReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.userUIDs = data.userUIDs;
                this.mailInput = data.mailInput;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        userUIDs: number[] = [];
        mailInput: Models.MailInput|null = null;
    }

    export class PInsertEventMailReq {
        constructor(data?: PInsertEventMailReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.eventMail = data.eventMail;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        eventMail: Models.EventMail|null = null;
    }

    export class PUpdateEventMailReq {
        constructor(data?: PUpdateEventMailReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.eventMail = data.eventMail;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        eventMail: Models.EventMail|null = null;
    }

    export class PRemoveEventMailReq {
        constructor(data?: PRemoveEventMailReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.eventMail = data.eventMail;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        eventMail: Models.EventMail|null = null;
    }

    export class PSendChattingMessageReq {
        constructor(data?: PSendChattingMessageReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.chattingMessage = data.chattingMessage;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        chattingMessage?: Models.ChattingMessage|null = null;
    }

    export class PTestReq {
        constructor(data?: PTestReq) {
            if (data) {
                this.token = data.token;
                this.reqPathName = data.reqPathName;
                this.userId = data.userId;
            }
        }

        token?: string|null = null;
        reqPathName?: string|null = null;
        userId: string = '';
    }

}
