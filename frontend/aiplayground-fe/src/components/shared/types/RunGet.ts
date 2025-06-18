import { Model } from "./Model";
import { Prompt } from "./Prompt";

export interface RunGet {
  id: number;
  promptName: string;
  expectedResult: string;
  actualResult: string;
  model: string;
  rating: number;
  userRating: number;
}
