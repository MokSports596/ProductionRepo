import Images from "./Images";

export const sampleData = [
  { id: '1', title: 'Mad...', position: '#1', season: '24', week: '+6', skins: '0', loks: '5', iconType: 'downward' },
  { id: '2', title: 'Team A', position: '#2', season: '23', week: '+4', skins: '2', loks: '3', iconType: 'upward' },
  { id: '3', title: 'Team B', position: '#3', season: '22', week: '+2', skins: '1', loks: '4', iconType: 'minus' },
  { id: '9', title: 'Team H', position: '#9', season: '16', week: '+9', skins: '5', loks: '8', iconType: 'upward' },
  { id: '4', title: 'Team C', position: '#4', season: '21', week: '+8', skins: '3', loks: '2', iconType: 'upward' },
  { id: '5', title: 'Team D', position: '#5', season: '20', week: '+3', skins: '1', loks: '6', iconType: 'downward' },
  { id: '6', title: 'Team E', position: '#6', season: '19', week: '+5', skins: '2', loks: '1', iconType: 'minus' },
  { id: '7', title: 'Team F', position: '#7', season: '18', week: '+7', skins: '4', loks: '4', iconType: 'upward' },
  { id: '8', title: 'Team G', position: '#8', season: '17', week: '+1', skins: '3', loks: '2', iconType: 'downward' },
  { id: '10', title: 'Team I', position: '#10', season: '15', week: '+2', skins: '2', loks: '0', iconType: 'minus' },
];

export const sampleDataHome = {
  title: 'Big...',
  position: '#3',
  season: '23',
  week: '+6',
  skins: '0',
  loks: '4',
};

export const teamData = [
  { title: 'Skin $/wk', perPlayer: '$100', total: '$400' },
  { title: 'LOK Leader', perPlayer: '$150', total: '$600' },
  { title: 'Super Bowl Winner', perPlayer: '$200', total: '$800' },
  {
    title: 'League',
    perPlayer: '$250',
    total1: '$30',
    total2: '$20',
    total3: '$10',
    position: ['1st', '2nd', '3rd'],
  },
];


export const gameData = [
  { image: Images.game1, number: 3, isActive: true },
  { image: Images.game2, number: 2, isActive: true },
  { image: Images.game3, number: 4, isActive: true },
  { image: Images.game4, number: 4, isActive: true },
  { image: Images.game5, number: 3, isActive: false },
];


export const allWeekData = {
  5: [
    {
      id: '8',
      team1: { image: Images.game2, shortName: 'SEA', score: 27 },
      team2: { image: Images.game1, shortName: 'NYJ', score: 35 },
      status: 'Final',
      history: ['+2 Win', '+1 LOK'],
    },
    {
      id: '5',
      team1: { image: Images.game3, shortName: 'KC', score: 28 },
      team2: { image: Images.game4, shortName: 'ATL', score: 20 },
      status: 'Semi-Final',
      history: ['+1 Win', '+1 HS'],
    },
    {
      id: '1',
      team1: { image: Images.game1, shortName: 'NYJ', score: 31 },
      team2: { image: Images.game2, shortName: 'SEA', score: 24 },
      status: 'Final',
      history: ['+1 Win', '+1 LOK', '+1 HS'],
      locked: true,
    },
    {
      id: '2',
      team1: { image: Images.game3, shortName: 'KC', score: 28 },
      team2: { image: Images.game4, shortName: 'ATL', score: 20 },
      status: 'Semi-Final',
      history: ['+1 Win', '+1 HS'],
    },
    {
      id: '7',
      team1: { image: Images.game2, shortName: 'SEA', score: 27 },
      team2: { image: Images.game1, shortName: 'NYJ', score: 35 },
      status: 'Final',
      history: ['+2 Win', '+1 LOK'],
    },
  ],
  6: [
    {
      id: '3',
      team1: { image: Images.game2, shortName: 'SEA', score: 27 },
      team2: { image: Images.game1, shortName: 'NYJ', score: 35 },
      status: 'Final',
      history: ['+2 Win', '+1 LOK'],
    },
    {
      id: '9',
      team1: { image: Images.game4, shortName: 'ATL', score: 14 },
      team2: { image: Images.game3, shortName: 'KC', score: 30 },
      status: 'Final',
      history: ['+1 Win', '+2 HS'],
      locked: true,
    },
    {
      id: '4',
      team1: { image: Images.game4, shortName: 'ATL', score: 14 },
      team2: { image: Images.game3, shortName: 'KC', score: 30 },
      status: 'Final',
      history: ['+1 Win', '+2 HS'],
      locked: true,
    },
  ],


};
export const WeekSkinData = [
  {
    rank: 1,
    team: 'BAT',
    points: '6 Pts',
    result: '(+HS)',
    isWinner: true,
    icons: [],
  },
  {
    rank: 1,
    team: 'M&M',
    points: '6 Pts',
    result: '',
    isWinner: true,
    icons: [],
  },
  {
    rank: 3,
    team: 'NJ',
    points: '3 Pts',
    result: '',
    isWinner: false,
    icons: [Images.game1],
  },
  {
    rank: 4,
    team: 'KRW',
    points: '2 Pts',
    result: '',
    isWinner: false,
    icons: [Images.game2],
  },
  {
    rank: 4,
    team: 'Lobster',
    points: '2 Pts',
    result: '(-LS)',
    isWinner: false,
    icons: [Images.game1, Images.game3],
  },
  {
    rank: 4,
    team: 'Orange',
    points: '2 Pts',
    result: '',
    isWinner: false,
    icons: [Images.game4, Images.game2],
  },
];

export const allWeeksHistoryData = {
  4: [
    {
      id: '1',
      team1: { name: 'BAT', score: 14, image: Images.game1 },
      team2: { name: 'KRW', score: 14, image: Images.game2 },
      status: 'Final',
    },
    {
      id: '2',
      team1: { name: 'BAT', score: 14, image: Images.game3 },
      team2: { name: 'KRW', score: 14, image: Images.game4 },
      status: 'Final',
    },
    {
      id: '6',
      team1: { name: 'NYJ', score: 20, image: Images.game5 },
      team2: { name: 'SEA', score: 14, image: Images.game2 },
      status: 'Final',
    },
    {
      id: '7',
      team1: { name: 'BAT', score: 14, image: Images.game1 },
      team2: { name: 'KRW', score: 14, image: Images.game2 },
      status: 'Final',
    },
    {
      id: '8',
      team1: { name: 'BAT', score: 14, image: Images.game3 },
      team2: { name: 'KRW', score: 14, image: Images.game4 },
      status: 'Final',
    },
  ],
  5: [
    {
      id: '3',
      team1: { name: 'NYJ', score: 20, image: Images.game5 },
      team2: { name: 'SEA', score: 14, image: Images.game2 },
      status: 'Final',
    },
    {
      id: '4',
      team1: { name: 'BAT', score: 14, image: Images.game1 },
      team2: { name: 'KRW', score: 14, image: Images.game2 },
      status: 'Final',
    },
    {
      id: '5',
      team1: { name: 'BAT', score: 14, image: Images.game3 },
      team2: { name: 'KRW', score: 14, image: Images.game4 },
      status: 'Final',
    },
  ],
};