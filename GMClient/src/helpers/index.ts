import { Models } from 'src/ngel/data/models';
import { encode } from '@msgpack/msgpack/dist/encode';
import { decode } from '@msgpack/msgpack/dist/decode';
export namespace Helpers {

    export function getParameterByName(name: string, url = window.location.href): string {
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
    
        if (!results)
            return '';
    
        if (!results[2])
            return '';
    
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }

    export function setLinearMenus(user: Models.SignInUser): Models.SignInUser {
        
        if (0 < user.menus.length) {
            for (let i = 0; i < user.menus.length; i++) {
                const menuDepth1 = user.menus[i];
                if (!menuDepth1)
                    continue;
    
                if (!menuDepth1.path)
                    continue;
    
                user.menusLinear.push(menuDepth1);
                if (1 > menuDepth1.children.length)
                    continue;
                    
                for (let j = 0; j < menuDepth1.children.length; j++) {
                    const menuDepth2 = menuDepth1.children[j];
                    if (!menuDepth2)
                        continue;
    
                    if (!menuDepth2.path)
                        continue;
    
                    user.menusLinear.push(menuDepth2);
                    if (1 > menuDepth2.children.length)
                        continue;
    
                    for (let k = 0; k < menuDepth2.children.length; k++) {
                        const menuDepth3 = menuDepth2.children[k];
                        if (!menuDepth3)
                            continue;
    
                        if (!menuDepth3.path)
                            continue;
    
                        user.menusLinear.push(menuDepth3);
                    }
                }
            }
        }

        return user;
    }

    export function encodeMessagePack(data?: object|null): string {

        let messagePack: string = '';

        if (!data)
            return messagePack;

        try {
            const encodedData = encode(data);
            const encodedDataBuffer = Buffer.from(encodedData.buffer, encodedData.byteOffset, encodedData.byteLength);
            messagePack = encodedDataBuffer.toString('base64');
        } catch (error) {
            console.error(error);
        }

        return messagePack;
    }

    export function decodeMessagePack<T>(dataString?: string|null): T|null {
        if (!dataString)
            return null;

        let decodedData: T|null = null;

        try {
            const decodedDataBuffer = Buffer.from(dataString, 'base64');
            decodedData = decode(decodedDataBuffer) as T;
        } catch (error) {
            console.error(error);
        }

        return decodedData;
    }

    export function setCookie(cname: string, cvalue: string, exdays?: number) {
        if ('undefined' === typeof document)
            return;

        try {
            let expires = "";
            if (exdays) {
                const d = new Date();
                d.setTime(d.getTime() + (exdays*24*60*60*1000));
                expires = "expires="+ d.toUTCString();
            }
            
            document.cookie = cname + "=" + cvalue + ";" + (expires ? expires + ";" : "") + "path=/";
        } catch (error) {
            console.error(error);
        }
    }

    export function setCookie30Min(cname: string, cvalue: string) {
        try {
            const d = new Date();
            d.setTime(d.getTime() + (30 * 60 * 1000));
            let expires = "expires="+ d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        } catch (error) {
            console.error(error);
        }
    }

    export function getCookie(cname?: string|null): string {
        if ('undefined' === typeof document)
            return '';

        try {
            let name = cname + "=";
            let decodedCookie = decodeURIComponent(document.cookie);
            let ca = decodedCookie.split(';');
            for(let i = 0; i <ca.length; i++) {
              let c = ca[i];
              while (c.charAt(0) == ' ') {
                c = c.substring(1);
              }
              if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
              }
            }
        } catch (error) {
            console.error(error);
        }
        return "";
    }

    export function getIntFromString (str: string, defaultValue?: number): number {
        str = str.replace(/[^0-9]/g, '');
        if (!str)
            return (defaultValue ?? 0);

        return parseInt(str);
    }

    export function getIntFromStringArray (strArray: string[], index: number, defaultValue?: number): number {
        if (!strArray)
            return defaultValue ?? 0;

        if (strArray.length <= index)
            return defaultValue ?? 0;

        const str = strArray[index];

        return getIntFromString(str, defaultValue ?? 0);
    }

}