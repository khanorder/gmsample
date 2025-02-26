import { ReactElement, useRef, useEffect } from 'react';
import { dayjs } from '@helpers/localizedDayjs';
import AdapterJalaali from '@date-io/dayjs';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import dynamic from 'next/dynamic';
const TextField = dynamic(() => import('@mui/material/TextField'), { ssr: false });

export interface DatePickerProps {
    label: string;
    onChange: (date: string|null) => void;
    value: string | null;
    format?: string;
    enableTimePicker?: boolean;
}

const DateTimePickers = ({ label, value, onChange, format = "YYYY-MM-DD", enableTimePicker = false }: DatePickerProps): ReactElement => {
    const firstRender = useRef(true);

    useEffect(() => {
        if (firstRender.current)
            firstRender.current = false;

    }, [firstRender]);

    return (
        <LocalizationProvider dateAdapter={AdapterJalaali} adapterLocale="ko">
            {
                enableTimePicker
                    ?
                        <DateTimePicker
                            label={label}
                            value={value}
                            inputFormat={format}
                            onChange={(date) => {onChange(dayjs(date).format(format))}}
                            renderInput={(props) => {
                                return <TextField {...props} size="small" fullWidth label={label} />;
                            }}
                        />
                    :
                        <DatePicker
                            label={label}
                            value={value}
                            inputFormat={format}
                            onChange={(date) => onChange(dayjs(date).format(format))}
                            renderInput={(props) => {
                                return <TextField {...props} size="small" fullWidth label={label} />;
                            }}
                        />
            }
        </LocalizationProvider>
    );
}

export default DateTimePickers;