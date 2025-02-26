import { Logger, createLogger, format, transports } from 'winston';
import 'winston-daily-rotate-file';
import { injectable } from 'inversify';
import DailyRotateFile, { DailyRotateFileTransportOptions } from 'winston-daily-rotate-file';
const { combine, timestamp, prettyPrint, printf } = format;

@injectable()
export class LoggerService {
    constructor () {
        const timezoned = () => {
            return new Date().toLocaleString('ko-kr', {
                timeZone: 'Asia/Seoul'
            });
        }
        
        const fileFormat = combine(
            timestamp({
                format: timezoned
            }), 
            prettyPrint(), 
            format.json()
        );
        
        const consoleFormat = combine(
            timestamp({
                format: timezoned
            }), 
            prettyPrint(), 
            printf(
                ({ level, message, timestamp }) => {
                    return `[${level}] - (${timestamp}) : ${JSON.stringify(message)}`
                }
            )
        );

        const fileNameDatePattern = 'YYYY-MM-DD';
        const zippedArchive = true;
        const maxSize = '20m';

        const commonFileTransportsOption: DailyRotateFileTransportOptions = {
            datePattern: fileNameDatePattern, 
            zippedArchive: zippedArchive, 
            maxSize: maxSize,
            format: fileFormat,
            filename: "logs/%DATE%/%DATE%_combined.log"
        }

        const errorFileTransportsOption: DailyRotateFileTransportOptions = {
            datePattern: fileNameDatePattern, 
            zippedArchive: zippedArchive, 
            maxSize: maxSize,
            format: fileFormat,
            filename: "logs/%DATE%/errors/%DATE%_error.log",
            level: "error"
        }

        const transportsOption: (DailyRotateFile | transports.ConsoleTransportInstance)[] = [
            new transports.DailyRotateFile(errorFileTransportsOption),
            new transports.DailyRotateFile(commonFileTransportsOption)
        ];

        if ("development" == process.env.NODE_ENV)
            transportsOption.push(new transports.Console({ format: consoleFormat }));

        this.logger = createLogger({transports: transportsOption});
    }
    
    public logger: Logger;
}