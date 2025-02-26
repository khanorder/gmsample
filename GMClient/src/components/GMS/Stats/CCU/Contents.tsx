import { ReactElement, useEffect, useCallback, useState, useRef } from 'react';
import { useAppDispatch, useAppSelector } from "@hooks/index";
import * as layoutsActions from '@store/reducers/layouts';
import dynamic from 'next/dynamic';
import { Models } from '@ngel/data/models';
import deepmerge from 'deepmerge';
import { Line } from 'react-chartjs-2';
import dayjs from 'dayjs';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

const Box = dynamic(() => import('@mui/material/Box'), { ssr: false });
const ToggleButton = dynamic(() => import('@mui/material/ToggleButton'), { ssr : true });

const colors = ['#35A1EE', '#EF6183', '#2CA05A', '#d62728', '#9467bd', '#8c564b', '#e377c2', '#7f7f7f', '#bcbd22', '#17becf', '#e41a1c', '#377eb8', '#4daf4a', '#984ea3', '#ff7f00', '#ffff33', '#a65628', '#f781bf', '#999999', '#66c2a5', '#fc8d62', '#8da0cb', '#e78ac3', '#a6d854', '#ffd92f', '#e5c494', '#b3b3b3', '#8dd3c7', '#ffffb3', '#bebada', '#fb8072', '#80b1d3', '#fdb462', '#b3de69', '#fccde5', '#d9d9d9', '#bc80bd', '#ccebc5', '#ffed6f',];

const pad = (date : Date) => {
  const timeString = 
  `${date.getFullYear()}-${("0"+(date.getMonth()+1)).slice(-2)}-${("0"+date.getDate()).slice(-2)}`+
  ` ${("0"+date.getHours()).slice(-2)}:${("0"+date.getMinutes()).slice(-2)}`;

  return timeString;
}

interface VisibleState{
  lobbyID: string,
  state: number;
}
interface ContentsProp {
  logs: Models.CCU[];
}

const options = {
  responsive: true,
  interaction: {
    mode: 'index' as const,
    intersect: false,
  },
  stacked: false,
  plugins: {
    title: {
      display: true,
      text: '실시간 접속자 수',
      font: {
        size: 20
      }
    },
    tooltip: {
      titleFont: {
        size: 16
      },
      bodyFont: {
        size: 14
      }
    },
    legend: {
      labels: {
        boxWidth: 20,
        font: {
          size: 16
        },
      }
    }
  },
}

const Contents = ({ logs  } : ContentsProp): ReactElement => {

  const dispatch = useAppDispatch();
  const [times, setTimes] = useState<string[]>([]);
  const [datas, setDatas] = useState<Models.CCU[][]>([]);
  const [lobbyVisibleState, setLobbyVisibleState] = useState<VisibleState[]>([]);
  const [toggleDisable, setToggleDisable] = useState<boolean>(false);
  
  const translateTimeUnit = (timeArr : string[]) => {
    if(timeArr.length >= 1){
      const lastDate = timeArr[timeArr.length-1];
      const firstDateObj = new Date(timeArr[0]);
      const lastDateObj = new Date(lastDate);
      const diffHour = dayjs(lastDate).diff(firstDateObj) / (60* 60 * 1000);
      
      if(diffHour >= 6){
        const filteredDates = timeArr.filter((dateStr, index) => {
          const timeDiffMinutes = (timeArr.length - index - 1) * 5; 
          const currentDate = new Date(dateStr);
          const timeDiffMilliseconds = currentDate.getTime() - lastDateObj.getTime();
          
          if(diffHour > 120) return timeDiffMinutes >= 5 && timeDiffMilliseconds % (60* 60 * 1000) === 0;
          if(diffHour > 48) return timeDiffMinutes >= 5 && timeDiffMilliseconds % (30* 60 * 1000) === 0;
          if(diffHour > 24) return timeDiffMinutes >= 5 && timeDiffMilliseconds % (10* 60 * 1000) === 0;
          if(diffHour > 12) return timeDiffMinutes >= 5 && timeDiffMilliseconds % (5* 60 * 1000) === 0;
          if(diffHour > 6) return timeDiffMinutes >= 5 && timeDiffMilliseconds % (3* 60 * 1000) === 0;
          
        })
        filteredDates.push(lastDate);
        return filteredDates;
      }
      return timeArr;
    }
    return timeArr;
  }

  useEffect(() => {
    const { lobbyIdsSet, timesSet } = logs.reduce((sets, item) => {
      sets.lobbyIdsSet.add(item.LobbyID);
      sets.timesSet.add(`${pad(item.CreateAt)}`);
      return sets;
    }, { lobbyIdsSet: new Set<string>(), timesSet: new Set<string>() });
    const lobbyIdsArr = Array.from(lobbyIdsSet);
    const timesArr =  translateTimeUnit(Array.from(timesSet).sort((a, b) => a > b ? 1 : -1));
    
    const result : Models.CCU[][] = [];
    let count = 1;
    for (let i = 0; i < lobbyIdsArr.length; i++) {
      const extractData: Models.CCU[] = [];
      
      for (let j = 0; j < timesArr.length; j++) {
        const foundData = logs.find(item => item.LobbyID === lobbyIdsArr[i] && pad(item.CreateAt) === timesArr[j]);
        if (foundData) {
          extractData.push(foundData);
        } else {
          extractData.push(new Models.CCU({ ID: (logs.length + count++), LobbyID: lobbyIdsArr[i], Total: 0, CreateAt: new Date(timesArr[j]) }));
        }
      }
  
      result.push(extractData);
    }
    
    const lobbyDatas : VisibleState[] = [];
    for(let i = 0; i < lobbyIdsArr.length; i++){
      const lobbyData: VisibleState = {lobbyID : lobbyIdsArr[i], state: 1};
      
      lobbyDatas.push(lobbyData);
    }
    
    if(0 < lobbyDatas?.length) setLobbyVisibleState(lobbyDatas);
    setDatas(result);
    setTimes(timesArr);
    
    }, [logs])

  const onChangeState = useCallback((index: number) => {
    
    setToggleDisable(true);
    setTimeout(() => {
      setToggleDisable(false);
    }, 500);
  
    setLobbyVisibleState((prev) => {
    prev[index].state =  prev[index].state == 1 ? 0 : 1;
    return deepmerge([], prev);
    })

  }, []);

  const contents = useCallback((): ReactElement => {
    if(datas && 0 < datas.length){
      const toggleButtonList: ReactElement[] = [];
      const visibleList = [];
      for(let j = 0 ; j < lobbyVisibleState.length; j++){
        if(lobbyVisibleState[j].state) visibleList.push(j);
        
        toggleButtonList.push(<ToggleButton key={`toggle-${j}`} sx={{ marginLeft: 1, marginBottom: 1}} disabled={toggleDisable} value={lobbyVisibleState[j].state} onChange={()=> onChangeState(j)}>{lobbyVisibleState[j].lobbyID}</ToggleButton>)
      }
      
      const series = [];
      if(0 < visibleList.length){
        for(let i = 0 ; i < datas.length ; i++){
          if(!(visibleList.includes(i))) continue;
          
          const data : Models.CCU[] = datas[i];
          const totalDatas = data.map(element => {return element.Total});
          series.push({ 
            label: data[0].LobbyID, fill: false,
            data: totalDatas, borderColor: colors[i] ?? colors[colors.length-1],
            backgroundColor: colors[i] ?? colors[colors.length-1],
          })
        }
      }

      const dataset = {
        labels: series.length > 0 ? times : [],
        datasets: series 
      }
      const result : ReactElement = (<>
      <Box sx={{ display:'flex', flexWrap: 'wrap', padding: 1, margin: 1 }}>
        {toggleButtonList}
      </Box>
      <Box width={1920}>
        <Line options={options} height={500} width={1920} data={dataset}></Line>
      </Box>
      </>)

      
      return result;
    }
    else {
      return <Box sx={{ display: 'flex', justifyContent: 'center', 
      alignItems: 'center', minHeight: 500 }}>
        검색된 실시간 접속자 정보가 없습니다.
        </Box>
    }
  }, [datas, times, lobbyVisibleState, toggleDisable, onChangeState]);

  return (<Box>
    {contents()}
    </Box>
  );
};


export default Contents;