import {Attachment} from '../model';

export interface StationaryRequest {
  prizeReceiver: string;
  attachments: Attachment[];
}
